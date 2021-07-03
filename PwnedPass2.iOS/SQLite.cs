using System;
using System.IO;
using PwnedPass2.Interfaces;
using SQLite;
[assembly: Xamarin.Forms.Dependency(typeof(PwnedPass2.iOS.SQLite))]
namespace PwnedPass2.iOS
{
    public class SQLite : ISQLite
    {


        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "pwnedpass.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);

            File.Open(path, FileMode.OpenOrCreate);

            var conn = new SQLiteConnection(path);

            return conn;
        }
    }
}
