using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvailablePCs
{
    public class Lab : INotifyPropertyChanged
    {
        private string name;
        private ObservableCollection<LabInfo> info;
        private ObservableCollection<Computer> computers;

        public Lab()
        {
            this.name = "";
            this.info = new ObservableCollection<LabInfo>();
            this.computers = new ObservableCollection<Computer>();            
        }

        public Lab(string n, ObservableCollection<LabInfo> i, ObservableCollection<Computer> c)
        {
            this.name = n;
            this.info = i;
            this.computers = c;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        public string Name
        {
            get { return this.name; }
            set
            {
                if (value != this.name)
                {
                    this.name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        public ObservableCollection<LabInfo> Info
        {
            get { return this.info; }
            set
            {
                if (value != this.info)
                {
                    this.info = value;
                    NotifyPropertyChanged("Info");
                }
            }
        }

        public ObservableCollection<Computer> Computers
        {
            get { return this.computers; }
            set
            {
                if (value != this.computers)
                {
                    this.computers = value;
                    NotifyPropertyChanged("Computers");
                }
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
