using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AP.Data;

namespace AP.Business
{
    public interface IUserRoleBusiness
    {
        IEnumerable<UserRole> GetAll();
        UserRole GetById(int userId, int roleId);
        void Create(UserRole userRole);
        void Update(UserRole userRole);
        void Delete(int userId, int roleId);
    }

}
