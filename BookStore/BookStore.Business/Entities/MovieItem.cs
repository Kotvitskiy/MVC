using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Store.Business.Entities
{
    public class MovieItem : BaseEntity
    {
        public TimeSpan Duration { get; set; }

        public DisplayResolution DisplayResolution { get; set; }
    }
}
