using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Business.Entities;

namespace Store.Business.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Save(IEnumerable<T> items);
        IEnumerable<T> GetAll();
    }
}
