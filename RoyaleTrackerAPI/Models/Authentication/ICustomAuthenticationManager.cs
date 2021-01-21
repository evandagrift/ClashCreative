using RoyaleTrackerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoyaleTrackerAPI
{
    public interface ICustomAuthenticationManager
    {
        string Authenticate(string username, string password, TRContext context);
        
        IDictionary<string, Tuple<string,string>> Tokens { get; }

    }
}
