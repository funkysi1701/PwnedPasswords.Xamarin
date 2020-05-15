using PwnedPass2.Interfaces;
using PwnedPasswords.Core;
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

            this.database.CreateTable<HIBP>();
            this.database.CreateTable<HIBPTotals>();
            this.database.CreateTable<LastEmail>();
        }

        public int SaveDataBreach(HIBP hibpmodel)
        {
            lock (Locker)
            {
                if (hibpmodel.Id != 0)
                {
                    this.database.Delete(hibpmodel);
                    this.database.Insert(hibpmodel);
                    return hibpmodel.Id;
                }
                else
                {
                    this.database.Delete(hibpmodel);
                    return this.database.Insert(hibpmodel);
                }
            }
        }

        public void EmptyDataBreach()
        {
            this.database.DropTable<HIBP>();
            this.database.CreateTable<HIBP>();
        }

        public HIBPTotals GetHIBP()
        {
            lock (Locker)
            {
                return (from c in this.database.Table<HIBPTotals>()
                        orderby c.Id descending
                        select c).FirstOrDefault();
            }
        }

        public int SaveHIBP(HIBPTotals hibp)
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

        public IEnumerable<HIBP> Get(int id)
        {
            lock (Locker)
            {
                return (from c in this.database.Table<HIBP>().Take(id)
                        select c).ToList();
            }
        }

        public IEnumerable<HIBP> GetAll()
        {
            lock (Locker)
            {
                return (from c in this.database.Table<HIBP>()
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