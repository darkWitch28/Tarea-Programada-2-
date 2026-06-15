using AP.Data;
using AP.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace AP.Business
{
    public class UserRoleBusiness
    {
        private readonly UserRoleRepository _repository;

        public UserRoleBusiness()
        {
            _repository = new UserRoleRepository();
        }

        public IEnumerable<UserRole> GetAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable<UserRole> GetUserRoles(int? userId = null)
        {
            var all = _repository.GetAll();
            if (userId.HasValue)
                return all.Where(ur => ur.UserId == userId.Value);
            return all;
        }

        public UserRole GetById(int userId, int roleId)
        {
            return _repository.GetAll()
                .FirstOrDefault(ur => ur.UserId == userId && ur.RoleId == roleId);
        }

        public void SaveOrUpdate(UserRole userRole)
        {
            using (var context = new FoodbankEntities())
            {
                var existing = context.UserRoles
                    .FirstOrDefault(ur => ur.UserId == userRole.UserId && ur.RoleId == userRole.RoleId);

                if (existing == null)
                {
                    context.UserRoles.Add(userRole);
                }
                else
                {
                    existing.Description = userRole.Description;
                }

                context.SaveChanges();
            }
        }

        public void Delete(int userId, int roleId)
        {
            _repository.Delete(userId, roleId);
        }
    }
}