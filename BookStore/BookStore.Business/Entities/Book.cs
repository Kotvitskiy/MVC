using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Business.Entities
{
    public class Book
    {
        public string Name { get; set; }

        public string PublishingHouse { get; set; }
        
        public bool IsAvailable { get; set; }

        public bool IsNew { get; set; }
    }
}
