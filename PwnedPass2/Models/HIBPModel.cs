﻿using SQLite;
using System;
using System.Collections.Generic;

namespace PwnedPass2.Models
{
    public class HIBPModel
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }

        public string Name { get; set; }

        public string Domain { get; set; }

        public DateTime BreachDate { get; set; }

        public DateTime AddedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public int PwnCount { get; set; }

        public string Description { get; set; }

        public bool IsVerified { get; set; }

        public bool IsFabricated { get; set; }

        public bool IsSensitive { get; set; }

        public bool IsRetired { get; set; }

        public bool IsSpamList { get; set; }

        public string LogoType { get; set; }
    }
}