using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Student_Management.DAL;

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
    }
}
