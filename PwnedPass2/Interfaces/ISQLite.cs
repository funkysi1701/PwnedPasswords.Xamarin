using SQLite;

namespace PwnedPass2.Interfaces
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}