using RoyaleTrackerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoyaleTrackerAPI.Repos
{
    public class UserRepo : IUserRepo
    {
        private ClashContext context;
        
        public UserRepo(ClashContext c)
        {
            context = c;
        }
        public List<User> GetAllUsers()
        {

            return context.Users.ToList();
        }
        public void AddUser(User user)
        {
            context.Add(user);
        }

        public void DeleteUser(string username)
        {
            User user = GetUserByUsername(username);
            context.Users.Remove(user);
        }

        public User GetUserByUsername(string username)
        {
            return context.Users.Find(username);
        }

        public string GetUserToken(string username)
        {
            //get user by username
            User user = GetUserByUsername(username);

            if (user.Token == null)
            {
                user.Token = Guid.NewGuid().ToString();
                context.SaveChanges();
            }
            return user.Token;
        }
    }
}
