using Microsoft.EntityFrameworkCore;
using MobileStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileStore.Patterns
{
    public class SQLOrdersRepository : IRepository<Order>
    {
        private MobileContext db;
        public SQLOrdersRepository(MobileContext mobileContext)
        {
            db = mobileContext;
        }
        public void Create(Order item)
        {
            db.Orders.Add(item);
        }

        public void Delete(Order item)
        {
            Order book = db.Orders.Find(item);
            if (book != null)
                db.Orders.Remove(book);
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

        public Order GetItem(int id)
        {
            return db.Orders.Find(id);
        }

        public IEnumerable<Order> GetItemsList()
        {
            return db.Orders;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Order item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
