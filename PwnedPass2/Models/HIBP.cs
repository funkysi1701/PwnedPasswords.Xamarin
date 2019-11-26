﻿using SQLite;

namespace PwnedPass2.Models
{
    public class HIBP
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public long TotalAccounts { get; set; }

        public int TotalBreaches { get; set; }
    }
}