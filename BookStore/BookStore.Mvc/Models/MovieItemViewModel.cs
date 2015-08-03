using Store.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.Mvc.Models
{
    public class MovieItemViewModel
    {
        public string Name { get; set; }

        public TimeSpan Duration { get; set; }

        public DisplayResolution DisplayResolution { get; set; }
    }
}