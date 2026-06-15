using AP.Data;
using AP.Repositories;
using System.Collections.Generic;

namespace AP.Business
{
    public class RoleBusiness
    {
        private readonly RoleRepository _roleRepository;

        public RoleBusiness()
        {
            _roleRepository = new RoleRepository();
        }

        public IEnumerable<Role> GetRoles(int id = 0)
        {
            if (id == 0)
                return _roleRepository.GetAll();
            else
                return new List<Role> { _roleRepository.GetById(id) };
        }

        public bool SaveOrUpdate(Role role)
        {
            if (role.RoleId == 0)
                _roleRepository.Add(role);
            else
                _roleRepository.Update(role);

            return true;
        }

        public void Delete(int id)
        {
            _roleRepository.Delete(id);
        }
    }
}