using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Business.Search
{
    public interface ISearchService<T>
    {
        IEnumerable<T> Search(string searchString);
        void BuildIndex(IEnumerable<T> items);

    }
}
