using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.Mvc.Models
{
    [MetadataType(typeof(BookViewModelAttributes))]
    public class BookViewModel
    {
        public bool IsAvailable { get; set; }

        public bool IsNew { get; set; }
    }

    public class BookViewModelAttributes
    {
        public bool IsAvailable { get; set; }

        [UIHint("SwitchButton")]
        public bool IsNew { get; set; }
    }
}