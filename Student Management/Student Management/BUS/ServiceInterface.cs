using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Student_Management.DAL;
using System.Collections.ObjectModel;

namespace Student_Management.BUS
{
    public enum state { incorrectPassword, accountIsNotExist, isStudent, isManager };

    class ServiceInterface
    {
        DataAccess handle = new DataAccess();
        public state checkStateAccess(string username, string password)
        {
            if (!handle.isAccountExist(username))
                return state.accountIsNotExist;
            int sta = handle.comparePassword(username, password);
            if (sta == 2)
                return state.isStudent;
            else if (sta == 3)
                return state.isManager;
            return state.incorrectPassword;
        }

        internal bool isClassExist(string path)
        {
            return handle.isClassExist(path);
        }

        internal ObservableCollection<Student> addClass(string filePath)
        {
            return returnClass(handle.addClass(filePath));
        }

        private ObservableCollection<Student> returnClass(List<string[]> getClass)
        {
            ObservableCollection<Student> _getClass = new ObservableCollection<Student>();

            foreach(string[] _class in getClass)
            {
                _getClass.Add(new Student()
                {
                    STT = Int32.Parse(_class[0]),
                    MSSV = _class[1],
                    HOTEN = _class[2],
                    GIOITINH = _class[3],
                    CMND = _class[4],
                    NGAYSINH = _class[5],
                    DIACHI = _class[6],
                    MALOP = _class[7]
                });
            }
            return _getClass;
        }

        internal ObservableCollection<Schedule> addSchedule(string filePath)
        {
            return returnSchedule(handle.addSchedule(filePath));
        }

        private ObservableCollection<Schedule> returnSchedule(List<string[]> getSchedule)
        {
            ObservableCollection<Schedule> _getSchedule = new ObservableCollection<Schedule>();

            foreach (string[] _schedule in getSchedule)
            {
                _getSchedule.Add(new Schedule()
                {
                    STT = Int32.Parse(_schedule[0]),
                    MAMON = _schedule[1],
                    TENMON = _schedule[2],
                    PHONGHOC = _schedule[3],
                    MALOP = _schedule[4]
                });
            }
            return _getSchedule;
        }
    }
}
