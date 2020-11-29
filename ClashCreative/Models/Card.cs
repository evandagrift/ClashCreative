﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClashCreative.Models
{
    public class Card
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get;set; }
        public string Name { get; set; }
        public string Url { get { return IconUrls["medium"]; } set { } }


        [NotMapped]
        public IDictionary<string, string> IconUrls { get; set; }
    }
}
