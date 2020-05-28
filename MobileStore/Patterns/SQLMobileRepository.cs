using Microsoft.EntityFrameworkCore;
using MobileStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileStore.Patterns
{
    public class SQLMobileRepository:IRepository<Phone>
    {
        private MobileContext db;
        public SQLMobileRepository(MobileContext mobileContext)
        {
            db = mobileContext;
        }
        public void Create(Phone item)
        {
            db.Phones.Add(item);
        }

        public void Delete(Phone item)
        {
            Phone book = db.Phones.Find(item);
            if (book != null)
                db.Phones.Remove(book);
        }
        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Phone GetItem(int id)
        {
            return db.Phones.Find(id);
        }

        public IEnumerable<Phone> GetItemsList()
        {
            return db.Phones;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Phone item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
