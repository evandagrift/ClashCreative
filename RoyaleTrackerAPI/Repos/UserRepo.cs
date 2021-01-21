using RoyaleTrackerAPI.Models;
using RoyaleTrackerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoyaleTrackerAPI.Repos
{
    public class UserRepo : IUserRepo
    {
        private TRContext context;
        
        public UserRepo(TRContext c)
        {
            context = c;
        }

        //create
        public void AddUser(User user)
        {
            context.Add(user);
        }

        //read byID
        public User GetUserByUsername(string username)
        {
            return context.Users.Find(username);
        }

        //read all
        public List<User> GetAllUsers()
        {

            return context.Users.ToList();
        }


        //delete
        public void DeleteUser(string username)
        {
            User user = GetUserByUsername(username);
            if (user != null)
            {
                context.Users.Remove(user);
            }
        }

        //token
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
