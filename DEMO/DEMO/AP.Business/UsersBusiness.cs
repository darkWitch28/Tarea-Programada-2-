using AP.Data;
using AP.Repositories;
using System;
using System.Collections.Generic;

namespace AP.Business
{
    public class UsersBusiness
    {
        private readonly UserRepository _userRepository;

        public UsersBusiness()
        {
            _userRepository = new UserRepository();
        }

        public IEnumerable<User> GetUsers(int id = 0)
        {
            if (id == 0)
                return _userRepository.GetAll();

            var user = _userRepository.GetById(id);
            return user != null
                ? new List<User> { user }
                : new List<User>();
        }

        public bool SaveOrUpdate(User user)
        {
            if (user.UserId == 0)
            {
                user.CreatedAt = DateTime.Now;
                user.IsActive = true;
                _userRepository.Add(user);
            }
            else
            {
                _userRepository.Update(user);
            }

            return true;
        }

        public void Delete(int id)
        {
            _userRepository.Delete(id);
        }
    }
}
