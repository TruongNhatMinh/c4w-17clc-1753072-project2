using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Student_Management.BUS
{
    public class Components : INotifyPropertyChanged
    {
        private string currentAccount = "";
        public string CurrentAccount
        {
            get { return currentAccount; }
            set { currentAccount = value; }
        }
        private ObservableCollection<Student> _class = new ObservableCollection<Student>();
        private ObservableCollection<Schedule> _schedule = new ObservableCollection<Schedule>();
        private ObservableCollection<Scoreboard> _scoreboard = new ObservableCollection<Scoreboard>();

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Student> NewClass
        {
            get { return _class; }
            set { _class = value; OnPropertyChanged("NewClass"); }
        }

        public ObservableCollection<Schedule> NewSchedule
        {
            get { return _schedule; }
            set { _schedule = value; OnPropertyChanged("NewSchedule"); }
        }

        public ObservableCollection<Scoreboard> NewScoreboard
        {
            get { return _scoreboard; }
            set { _scoreboard = value; OnPropertyChanged("NewScoreboard"); }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
