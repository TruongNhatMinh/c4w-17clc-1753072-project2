﻿using System;
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

        internal List<string> nameClass()
        {
            return handle.nameClass();
        }

        internal ObservableCollection<Student> viewClass(string nClass)
        {
            return returnClass(handle.viewClass(nClass));
        }

        internal List<string> nameCourses(string nClass)
        {
            return handle.nameCourses(nClass);
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

        internal void addStudent(List<string> information)
        {
            handle.addStudent(information);
        }

        internal ObservableCollection<Schedule> addSchedule(string filePath)
        {
            return returnSchedule(handle.addSchedule(filePath));
        }

        internal ObservableCollection<Schedule> viewSchedule(string nClass)
        {
            return returnSchedule(handle.viewSchedule(nClass));
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

        internal ObservableCollection<Scoreboard> addScoreboard(string filePath)
        {
            return returnScoreboard(handle.addScoreboard(filePath));
        }

        internal ObservableCollection<Scoreboard> viewScoreboard(string nClass, string nCourses)
        {
            return returnScoreboard(handle.viewScoreboard(nClass, nCourses));
        }

        private ObservableCollection<Scoreboard> returnScoreboard(List<string[]> getScoreboard)
        {
            ObservableCollection<Scoreboard> _getScoreboard = new ObservableCollection<Scoreboard>();

            foreach (string[] _schedule in getScoreboard)
            {
                _getScoreboard.Add(new Scoreboard()
                {
                    STT = Int32.Parse(_schedule[0]),
                    MSSV = _schedule[1],
                    HOTEN = _schedule[2],
                    MAMON = _schedule[3],
                    DIEMGK = float.Parse(_schedule[4]),
                    DIEMCK = float.Parse(_schedule[5]),
                    DIEMKHAC = float.Parse(_schedule[6]),
                    DIEMTB = float.Parse(_schedule[7]),
                    MALOP = _schedule[8]
                });
            }
            return _getScoreboard;
        }

        internal void editMark(List<string> mark)
        {
            handle.editMark(mark);
        }

        internal bool modifyPassword(string account, string oldPassword, string newPassword)
        {
            return handle.modifyPassword(account, oldPassword, newPassword);
        }
    }
}
