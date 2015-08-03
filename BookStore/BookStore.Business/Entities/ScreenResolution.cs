using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Business.Entities
{
    public class DisplayResolution
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public override string ToString()
        {
            return String.Format("{0} x {1}", Width, Height);
        }
    }
}
