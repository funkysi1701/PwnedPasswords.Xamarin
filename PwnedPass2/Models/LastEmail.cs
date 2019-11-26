using SQLite;

namespace PwnedPass2.Models
{
    public class LastEmail
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public string Email { get; set; }
    }
}