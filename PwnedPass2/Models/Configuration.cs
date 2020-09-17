﻿using Newtonsoft.Json;
using PwnedPass2.Interfaces;

namespace PwnedPass2.Models
{
    public class Configuration : IConfiguration
    {
        [JsonConstructor]
        public Configuration()
        {

        }

        public bool Beta { get; set; }
    }
}
