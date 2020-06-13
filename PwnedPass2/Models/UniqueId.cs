using SQLite;
using System;

namespace PwnedPass2.Models
{
    public class UniqueId
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        public UniqueId()
        {
            Id = Guid.NewGuid();
        }
    }
}
