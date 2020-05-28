using MobileStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileStore.Patterns
{
    public class UnitOfWork : IDisposable
    {
        private MobileContext db;
        private SQLMobileRepository SQLMobileRepository;
        private SQLOrdersRepository SQLOrdersRepository;
        public UnitOfWork(MobileContext mobileContext)
        {
            db = mobileContext;
        }
        public SQLMobileRepository Mobiles
        {
            get
            {
                if (SQLMobileRepository == null)
                    SQLMobileRepository = new SQLMobileRepository(db);
                return SQLMobileRepository;
            }
        }
        public SQLOrdersRepository Orders
        {
            get
            {
                if (SQLOrdersRepository == null)
                    SQLOrdersRepository = new SQLOrdersRepository(db);
                return SQLOrdersRepository;
            }
        }
        public void Save()
        {
            db.SaveChanges();
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
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
