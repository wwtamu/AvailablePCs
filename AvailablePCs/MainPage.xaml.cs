using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace AvailablePCs
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class MainPage : LayoutAwarePage, INotifyPropertyChanged
    {
        private static MachineAvailabilityServiceReference.IService1 AvailabilityService;
        
        private ThreadPoolTimer _a_timer = null;
        private ThreadPoolTimer _delay = null;

        private static Utilities toolbox;
        private static Network network;
        private static Cache cache;

        private static bool have_cache;

        private ObservableCollection<Lab> labs = new ObservableCollection<Lab>();

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Lab> Labs
        {
            get { return this.labs; }
            set
            {
                if (value != this.labs)
                {
                    this.labs = value;
                    NotifyPropertyChanged("Labs");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
            DataContext = this;

            AvailabilityService = new MachineAvailabilityServiceReference.Service1Client();
            
            toolbox = new Utilities();
            network = new Network();
            cache = new Cache();

            Application.Current.Suspending += (sender, args) => OnSuspending();
            Application.Current.Resuming += (sender, o) => OnResuming();
            Window.Current.VisibilityChanged += Current_VisibilityChanged;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">
        /// Event data that describes how this page was reached. The 
        /// Parameter property is typically used to configure the page.
        /// </param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (AvailablePCs.SettingsView.GetCacheSetting())
            {
                have_cache = await cache.CheckCache();
                await Initialize(true);
            }
            else
            {
                await Initialize(false);
            }

            PostPopulateLab();

            _delay = ThreadPoolTimer.CreatePeriodicTimer(DelayedHandler, TimeSpan.FromMilliseconds(15000));
        }

        public async Task<bool> Initialize(bool use_cache)
        {   
            if (use_cache)
            {
                await cache.InitCache();
                
                labs = await cache.GetCache();

                if (!(have_cache = AvailablePCs.SettingsView.HaveCacheSetting()))
                {
                    //CacheProgress(true);
                    await Initialize(false);
                }
            }
            else
            {
                await PopulateLabs();
                if (!(have_cache = await cache.CheckCache()))
                {   
                    await cache.StoreCache(labs);
                    //CacheProgress(false);
                }
            }
            return true;
        }

        /// <summary>
        /// Invoked when App is suspended.
        /// </summary>
        internal void OnSuspending()
        {
            _a_timer.Cancel();
        }

        /// <summary>
        /// Invoked when the App is resumed.
        /// </summary>
        internal void OnResuming()
        {
            _a_timer = ThreadPoolTimer.CreatePeriodicTimer(AvailabilityTimerElapsedHandler, TimeSpan.FromMilliseconds(2000));            
        }

        /// <summary>
        /// Invoked when the Apps visibility changes.
        /// Toggles the camera.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Current_VisibilityChanged(object sender, Windows.UI.Core.VisibilityChangedEventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timer"></param>
        public void DelayedHandler(ThreadPoolTimer timer)
        {
            this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                CheckAvailability();
            }).AsTask().Wait();
            _delay.Cancel();
            _a_timer = ThreadPoolTimer.CreatePeriodicTimer(AvailabilityTimerElapsedHandler, TimeSpan.FromMilliseconds(1000));            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timer"></param>
        public void AvailabilityTimerElapsedHandler(ThreadPoolTimer timer)
        {
            this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                CheckAvailability();
            }).AsTask().Wait();
        }

        /*
        private void CacheProgress(bool inProgress)
        {
            if (inProgress)
            {
                cache_bar.Visibility = Visibility.Visible;
            }
            else
            {
                cache_bar.Visibility = Visibility.Collapsed;
            }
        }
        */ 

        /// <summary>
        /// 
        /// </summary>
        private void PostPopulateLab()
        {
            loading_ring.IsActive = false;
            loading_ring.Visibility = Visibility.Collapsed;
            loading.Visibility = Visibility.Collapsed;

            cvsLabs.Source = labs;
            cvsLabsInfo.Source = labs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task<bool> PopulateLabs()
        {
            if (network.CheckForInternet())
            {
                bool exception_found = false;
                try
                {
                    var _labs = await AvailabilityService.GetAllLabsAsync();

                    foreach (MachineAvailabilityServiceReference.LabObject l in _labs)
                    {
                        Lab newLab = new Lab();
                        newLab.Name = l.LabName;
                        LabInfo newLabInfo = new LabInfo(l.LabName, l.LabBuilding, l.LabFloor, l.LabRoom, l.LabMachineCount, 0, ConvertTime(l.LabOpen), ConvertTime(l.LabClose), "Unknown"); //l.LabStatus
                        
                        try
                        {
                            var computers = await AvailabilityService.GetComputersInLabAsync(l.LabName);
                            var availability = await AvailabilityService.GetComputersAvailabilityAsync(l.LabName);
                            int i = 0;
                            int a = 0;
                            foreach (MachineAvailabilityServiceReference.MachineObject m in computers)
                            {
                                if (availability.ElementAt(i) == true)
                                {
                                    newLab.Computers.Add(new Computer(m.MachineName, Constants.available_image, availability.ElementAt(i), "ON", "Available"));
                                    a++;
                                }
                                else
                                {
                                    newLab.Computers.Add(new Computer(m.MachineName, Constants.inuse_image, availability.ElementAt(i), "ON", "InUse"));
                                }
                                i++;
                            }
                            newLabInfo.Available = a;
                            newLab.Info.Add(newLabInfo);
                            labs.Add(newLab);
                        }
                        catch (Exception)
                        {
                            exception_found = true;
                        }

                        if (exception_found) // allows awaitable method before exit
                        {
                            await toolbox.WaitablePromptMessage("Service is only available within the AD.SIU.EDU domain. \n\nIf off campus connect to the VPN via Network Connect or go to vpn.siu.edu.\n\n");
                            App.Current.Exit();
                        }
                    }
                }
                catch (Exception)
                {
                    exception_found = true;
                }

                if (exception_found) // allows awaitable method before exit
                {
                    await toolbox.WaitablePromptMessage("Service is only available within the AD.SIU.EDU domain. \n\nIf off campus connect to the VPN via Network Connect or go to vpn.siu.edu.\n\n");
                    App.Current.Exit();
                }
            }
            else
            {
                await toolbox.WaitablePromptMessage("No internet connection. Please connect and try again.");
                App.Current.Exit();
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="military_time"></param>
        /// <returns></returns>
        public string ConvertTime(String military_time)
        {
            string[] time_split = military_time.Split(new char[] { ':' }, StringSplitOptions.None);

            if (Convert.ToInt32(time_split[0]) < 12)
            {
                return Convert.ToInt32(time_split[0]) + ":" + time_split[1] + "am";
            }
            else
            {
                return (Convert.ToInt32(time_split[0]) - 12) + ":" + time_split[1] + "pm";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public async void CheckAvailability()
        {
            if (network.CheckForInternet())
            {
                bool exception_found = false;
                foreach (Lab lab in Labs)
                {
                    try
                    {
                        bool lab_availability = await AvailabilityService.GetLabAvailabilityAsync(lab.Name);
                        string temp;

                        if (lab_availability)
                        {
                            temp = "Open";
                        }
                        else
                        {
                            temp = "Closed";
                        }
                        if (lab.Info.FirstOrDefault().Status != temp)
                        {
                            lab.Info.FirstOrDefault().Status = temp;
                        }
                    }
                    catch (Exception)
                    {
                        exception_found = true;
                    }

                    if (exception_found) // allows awaitable method before exit
                    {
                        await toolbox.WaitablePromptMessage("Service is only available within the AD.SIU.EDU domain. \n\nIf off campus connect to the VPN via Network Connect or go to vpn.siu.edu.\n\n");
                        App.Current.Exit();
                    }

                    try
                    {
                        ObservableCollection<string> lab_computer_status = await AvailabilityService.GetComputersStatusAsync(lab.Name);
                        ObservableCollection<bool> lab_computer_availability = await AvailabilityService.GetComputersAvailabilityAsync(lab.Name);
                        int i = 0;
                        bool change = false;
                        foreach (Computer computer in lab.Computers)
                        {
                            if (computer.Status != lab_computer_status.ElementAt(i))
                            {
                                if (lab_computer_status.ElementAt(i) == "ON")
                                {
                                    lab.Computers.ElementAt(i).Status = "ON";
                                    lab.Computers.ElementAt(i).Image = Constants.available_image;
                                }
                                else if (lab_computer_status.ElementAt(i) == "OFF")
                                {
                                    lab.Computers.ElementAt(i).Status = "OFF";
                                    lab.Computers.ElementAt(i).Use = "OFF";
                                    lab.Computers.ElementAt(i).Availability = false;
                                    lab.Computers.ElementAt(i).Image = Constants.off_image;
                                }
                                else { }
                            }

                            if (computer.Availability != lab_computer_availability.ElementAt(i))
                            {
                                change = true;
                                lab.Computers.ElementAt(i).Availability = lab_computer_availability.ElementAt(i);
                                if (lab_computer_availability.ElementAt(i) == true)
                                {
                                    lab.Computers.ElementAt(i).Use = "Available";
                                    lab.Computers.ElementAt(i).Availability = true;
                                    lab.Computers.ElementAt(i).Image = Constants.available_image;
                                }
                                else
                                {
                                    if (lab.Computers.ElementAt(i).Status == "ON")
                                    {
                                        lab.Computers.ElementAt(i).Use = "In Use";
                                        lab.Computers.ElementAt(i).Availability = false;
                                        lab.Computers.ElementAt(i).Image = Constants.inuse_image;
                                    }
                                }
                            }

                            i++;
                        }
                        if (change)
                        {
                            lab.Info.FirstOrDefault().Available = lab.Computers.Where(x => x.Availability == true).Count();
                        }
                    }
                    catch (Exception)
                    {
                        exception_found = true;
                    }

                    if (exception_found) // allows awaitable method before exit
                    {
                        await toolbox.WaitablePromptMessage("Service is only available within the AD.SIU.EDU domain. \n\nIf off campus connect to the VPN via Network Connect or go to vpn.siu.edu.\n\n");
                        App.Current.Exit();
                    }
                }
            }
            else 
            {
                await toolbox.WaitablePromptMessage("This app requires internet connectivity on the AD.SIU.EDU domain. SIU-indoor or any internet connection with Network Connect VPN. Thank You.\n\n");
                App.Current.Exit();
            }
        }

        private void Lab_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var lab_name = ((StackPanel)sender).Tag as string;
            

            if (semanticLandscapeZoom.Visibility == Visibility.Visible)
            {
                semanticLandscapeZoom.IsZoomedInViewActive = true;
                zoomedInLandscapeGridView.ScrollIntoView(zoomedInLandscapeGridView.Items[GetComputerCountOfFirstInGroup(lab_name)], ScrollIntoViewAlignment.Leading);                
            }
            else
            {
                semanticPortraitZoom.IsZoomedInViewActive = true;
                portraitScrollViewer.ScrollToVerticalOffset(((labs.IndexOf(labs.Where(x => x.Name == lab_name).FirstOrDefault())*100) + (65 * (GetComputerCountOfFirstInGroup(lab_name)/4)) + 1));
            }
        }

        public int GetComputerCountOfFirstInGroup(string lab_name)
        {
            int sum = 0;
            for (int lab_index = 0; lab_index <= labs.IndexOf(labs.Where(x => x.Name == lab_name).FirstOrDefault()); lab_index++)
            {
                sum += labs.Where(x => x.Name == labs.ElementAt(lab_index).Name).FirstOrDefault().Info.FirstOrDefault().Total;
            }
            if (sum - labs.Where(x => x.Name == lab_name).FirstOrDefault().Info.FirstOrDefault().Total < 0)
                return 0;
            else
                return sum - labs.Where(x => x.Name == lab_name).FirstOrDefault().Info.FirstOrDefault().Total;
        }

        public T GetVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            T child = default(T);

            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                DependencyObject v = (DependencyObject)VisualTreeHelper.GetChild(parent, i);

                child = v as T;
                if (child == null)
                    child = GetVisualChild<T>(v);
                if (child != null)
                    break;
            }
            return child;
        }

        public T GetVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            T parent = default(T);
            DependencyObject v = (DependencyObject)VisualTreeHelper.GetParent(child);
            parent = v as T;
            if (parent == null)
                parent = GetVisualParent<T>(v);
            return parent;
        }
    }
}
