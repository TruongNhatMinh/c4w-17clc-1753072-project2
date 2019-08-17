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
        private ObservableCollection<Student> newClass = new ObservableCollection<Student>();
        private ObservableCollection<Schedule> newSchedule = new ObservableCollection<Schedule>();
        private ObservableCollection<Scoreboard> newScoreboard = new ObservableCollection<Scoreboard>();

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Student> NewClass
        {
            get { return newClass; }
            set { newClass = value; OnPropertyChanged("NewClass"); }
        }

        public ObservableCollection<Schedule> NewSchedule
        {
            get { return newSchedule; }
            set { newSchedule = value; OnPropertyChanged("NewSchedule"); }
        }

        public ObservableCollection<Scoreboard> NewScoreboard
        {
            get { return newScoreboard; }
            set { newScoreboard = value; OnPropertyChanged("NewScoreboard"); }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
