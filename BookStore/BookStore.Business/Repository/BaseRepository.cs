using Store.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Business.Repository
{
    public abstract class BaseRepository<T> : IRepository<T>
        where T : BaseEntity
    {
        public string FilePath { get; set; }

        public abstract void Save(IEnumerable<T> items);

        public abstract IEnumerable<T> GetAll();
    }
}
