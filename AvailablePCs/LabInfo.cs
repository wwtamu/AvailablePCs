using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvailablePCs
{
    public class LabInfo : INotifyPropertyChanged
    {
        private string name;
        private string building;
        private string room;
        private int floor;
        private int total;
        private int available;        
        private string open;
        private string close;
        private string status;

        public LabInfo()
        {
            this.name = "";
            this.building = "";
            this.room = "";
            this.floor = 0;
            this.total = 0;
            this.available = 0;            
            this.open = "";
            this.close = "";
            this.status = "";
        }

        public LabInfo(string n, string b, int f, string r, int t, int a, string o, string c, string s)
        {
            this.name = n;
            this.building = b;
            this.room = r;
            this.floor = f;
            this.total = t;
            this.available = a;
            this.open = o;
            this.close = c;
            this.status = s;
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

        public string Building
        {
            get { return this.building; }
            set
            {
                if (value != this.building)
                {
                    this.building = value;
                    NotifyPropertyChanged("Building");
                }
            }
        }

        public string Room
        {
            get { return this.room; }
            set
            {
                if (value != this.room)
                {
                    this.room = value;
                    NotifyPropertyChanged("Room");
                }
            }
        }
        public int Floor
        {
            get { return this.floor; }
            set
            {
                if (value != this.floor)
                {
                    this.floor = value;
                    NotifyPropertyChanged("Floor");
                }
            }
        }
        public int Total
        {
            get { return this.total; }
            set
            {
                if (value != this.total)
                {
                    this.total = value;
                    NotifyPropertyChanged("Total");
                }
            }
        }
        public int Available
        {
            get { return this.available; }
            set
            {
                if (value != this.available)
                {
                    this.available = value;
                    NotifyPropertyChanged("Available");
                }
            }
        }       
        public string Open
        {
            get { return this.open; }
            set
            {
                if (value != this.open)
                {
                    this.open = value;
                    NotifyPropertyChanged("Open");
                }
            }
        }
        public string Close
        {
            get { return this.close; }
            set
            {
                if (value != this.close)
                {
                    this.close = value;
                    NotifyPropertyChanged("Close");
                }
            }
        }
        public string Status
        {
            get { return this.status; }
            set
            {
                if (value != this.status)
                {
                    this.status = value;
                    NotifyPropertyChanged("Status");
                }
            }
        }

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
