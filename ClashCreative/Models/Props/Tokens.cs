using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClashCreative.Models.Props
{
    public class Tokens
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Token { get; set; }
        public string Username { get; set; }

        public string Role { get; set; }


    }
}
