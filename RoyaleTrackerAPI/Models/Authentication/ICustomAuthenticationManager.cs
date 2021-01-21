using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoyaleTrackerAPI
{
    public interface ICustomAuthenticationManager
    {
        string Authenticate(string username, string password, ClashContext context);
        
        IDictionary<string, Tuple<string,string>> Tokens { get; }

    }
}
