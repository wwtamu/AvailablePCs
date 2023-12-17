using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;

namespace AvailablePCs
{
    public class Cache
    {
        private Windows.Foundation.Collections.IPropertySet appSettings;
        private static StorageFolder labFolder = null;

        public Cache()
        {
            appSettings = ApplicationData.Current.LocalSettings.Values;
        }

        public async Task<bool> InitCache()
        {
            try
            {
                labFolder = await KnownFolders.DocumentsLibrary.CreateFolderAsync("SIU_Lab_Folder", CreationCollisionOption.OpenIfExists);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> StoreCache(ObservableCollection<Lab> labs)
        {
            if (labFolder == null)
            {
                await InitCache();
            }            
            try
            {
                foreach (Lab lab in labs)
                {
                    StorageFile labFile = await labFolder.CreateFileAsync(lab.Name + ".txt", CreationCollisionOption.ReplaceExisting);
                    await Windows.Storage.FileIO.WriteTextAsync(labFile, lab.Name + Environment.NewLine);
                    foreach (LabInfo li in lab.Info)
                    {
                        await Windows.Storage.FileIO.AppendTextAsync(labFile, li.Building + "|" +
                                                                              li.Floor + "|" + 
                                                                              li.Room + "|" +
                                                                              li.Total + "|" + 
                                                                              li.Available + "|" + 
                                                                              li.Open + "|" +
                                                                              li.Close + "|" +
                                                                              li.Status + Environment.NewLine);
                    }

                    foreach (Computer c in lab.Computers)
                    {
                        await Windows.Storage.FileIO.AppendTextAsync(labFile, c.Name + "|" + 
                                                                              c.Image + "|" +
                                                                              c.Availability + "|" +
                                                                              c.Status + "|" +
                                                                              c.Use + Environment.NewLine);
                    }
                }
            }
            catch (Exception) 
            {
                return false;
            }
            
            AvailablePCs.SettingsView.SetHaveCacheSetting(true);

            return true;
        }


        public async Task<bool> CheckCache()
        {
            if (labFolder == null)
            {
                await InitCache();
            }   
            try
            {
                var queryOptions = new QueryOptions(CommonFileQuery.DefaultQuery, new[] { ".txt" });
                var query = labFolder.CreateFileQueryWithOptions(queryOptions);
                
                if (await query.GetItemCountAsync() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ObservableCollection<Lab>> GetCache()
        {
            ObservableCollection<Lab> labs = new ObservableCollection<Lab>();
            try
            {
                var queryOptions = new QueryOptions(CommonFileQuery.DefaultQuery, new[] { ".txt" });
                var query = labFolder.CreateFileQueryWithOptions(queryOptions);
                var lab_files = await query.GetFilesAsync();

                if (lab_files.Count > 0)
                {
                    foreach (StorageFile lab_file in lab_files)
                    {
                        var lines = await FileIO.ReadLinesAsync(lab_file);
                        Lab new_lab = new Lab();
                        new_lab.Name = lines.FirstOrDefault();

                        string lab_info_string = lines[1];
                        string[] lab_info_split = lab_info_string.Split(new char[] { '|' }, StringSplitOptions.None);

                        LabInfo new_lab_info = new LabInfo(new_lab.Name,
                                                           lab_info_split[0],
                                                           Convert.ToInt32(lab_info_split[1]),
                                                           lab_info_split[2],
                                                           Convert.ToInt32(lab_info_split[3]),
                                                           Convert.ToInt32(lab_info_split[4]),
                                                           lab_info_split[5],
                                                           lab_info_split[6],
                                                           lab_info_split[7]);

                        new_lab.Info.Add(new_lab_info);

                        for (int i = 0; i < new_lab.Info.FirstOrDefault().Total; i++)
                        {   
                            string computer_string = lines[i + 2];
                            string[] computer_split = computer_string.Split(new char[] { '|' }, StringSplitOptions.None);
                            
                            ////////////////////////////////////////////////////////




                            ////////////////////////////////////////////////////////
                            Computer new_computer = new Computer(computer_split[0], 
                                                                 computer_split[1], 
                                                                 Convert.ToBoolean(computer_split[2].ToLower()),
                                                                 computer_split[3],
                                                                 computer_split[4]);

                            new_lab.Computers.Add(new_computer);
                        }
                        labs.Add(new_lab);
                    }
                }
                else
                {
                    AvailablePCs.SettingsView.SetHaveCacheSetting(false);
                }
            }
            catch (Exception)
            {
                AvailablePCs.SettingsView.SetHaveCacheSetting(false);
            }
            return labs;
        }
    }
}
