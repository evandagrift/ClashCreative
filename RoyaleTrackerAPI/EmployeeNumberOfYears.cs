using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoyaleTrackerAPI
{
    public class EmployeeNumberOfYears : IEmployeeNumberOfYears
    {
        public int Get(string name)
        {
            if(name == "test2")
            {
                return 21;
            }
            return 10;
        }
    }
}
