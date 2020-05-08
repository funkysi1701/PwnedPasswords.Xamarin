using PwnedPass2.Interfaces;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace PwnedPass2.Models
{
    public class Database
    {
        private static readonly object Locker = new object();
        private readonly SQLiteConnection database;

        public Database()
        {
            this.database = DependencyService.Get<ISQLite>().GetConnection();

            this.database.CreateTable<DataBreach>();
            this.database.CreateTable<HIBP>();
            this.database.CreateTable<LastEmail>();
        }

        public int SaveDataBreach(DataBreach databreach)
        {
            lock (Locker)
            {
                if (databreach.Id != 0)
                {
                    this.database.Delete(databreach);
                    this.database.Insert(databreach);
                    return databreach.Id;
                }
                else
                {
                    this.database.Delete(databreach);
                    return this.database.Insert(databreach);
                }
            }
        }

        public void EmptyDataBreach()
        {
            this.database.DropTable<DataBreach>();
            this.database.CreateTable<DataBreach>();
        }

        public HIBP GetHIBP()
        {
            lock (Locker)
            {
                return (from c in this.database.Table<HIBP>()
                        orderby c.Id descending
                        select c).FirstOrDefault();
            }
        }

        public int SaveHIBP(HIBP hibp)
        {
            lock (Locker)
            {
                if (hibp.Id != 0)
                {
                    this.database.Delete(hibp);
                    this.database.Insert(hibp);
                    return hibp.Id;
                }
                else
                {
                    return this.database.Insert(hibp);
                }
            }
        }

        public int SaveLastEmail(LastEmail lastemail)
        {
            lock (Locker)
            {
                try
                {
                    return this.database.Insert(lastemail);
                }
                catch
                {
                    return this.database.Update(lastemail);
                }
            }
        }

        public IEnumerable<DataBreach> Get(int id)
        {
            lock (Locker)
            {
                return (from c in this.database.Table<DataBreach>().Take(id)
                        select c).ToList();
            }
        }

        public IEnumerable<DataBreach> GetAll()
        {
            lock (Locker)
            {
                return (from c in this.database.Table<DataBreach>()
                        select c).ToList();
            }
        }

        public LastEmail GetLastEmail()
        {
            lock (Locker)
            {
                return (from c in this.database.Table<LastEmail>()
                        select c).FirstOrDefault();
            }
        }
    }
}