using RoyaleTrackerAPI.Models;
using RoyaleTrackerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoyaleTrackerAPI
{
    public class CustomAuthenticationManager : ICustomAuthenticationManager
    {
        //Note:Replace with DB Link
        private readonly IList<User> users = new List<User>
        {
            new User {Username = "test1", Password = "pass1", Role = "Admin"},
            new User {Username = "test2", Password = "pass2", Role = "User"}
        };

        private readonly IDictionary<string, Tuple<string,string>> tokens = 
            new Dictionary<string, Tuple<string, string>>();

        public IDictionary<string, Tuple<string, string>> Tokens => tokens;


        public string Authenticate(string username, string password, TRContext context)
        {
            User user = context.Users.Where(u => u.Username == username && u.Password == password).FirstOrDefault();
            

            //if no user is retrieved
            if(user == null)
            {
                return null;
            }

            if(user.Token == null)
            { 
                user.Token = Guid.NewGuid().ToString();
                context.Users.Add(user);
                context.SaveChangesAsync();
            }
            return user.Token;
        }
    }
}
