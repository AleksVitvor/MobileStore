using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileStore.Patterns
{
    interface IRepository<T>:IDisposable where T:class
    {
        IEnumerable<T> GetItemsList();
        T GetItem(int id);
        void Create(T item);
        void Delete(T item);
        void Update(T item);
        void Save();
    }
}
