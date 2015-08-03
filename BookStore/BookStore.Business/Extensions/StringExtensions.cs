using Store.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Store.Business.Extensions
{
    public static class StringExtensions
    {
        public static TimeSpan ToTimeSpan(this string s)
        {
              Regex myTimePattern = new Regex(@"^(\d+)(\:(\d+))+?$");
              if ( s == null ) throw new ArgumentNullException("s") ;
              Match m = myTimePattern.Match(s) ;
              if ( ! m.Success ) throw new ArgumentOutOfRangeException("s") ;
              string hh = m.Groups[1].Value ;
              string mm = m.Groups[3].Value.PadRight(2,'0') ;
              int hours   = int.Parse( hh ) ;
              int minutes = int.Parse( mm ) ;
              if ( minutes < 0 || minutes > 59 ) throw new ArgumentOutOfRangeException("s") ;
              TimeSpan value = new TimeSpan(hours , minutes , 0 ) ;
              return value ;
        }

        public static DisplayResolution ToDisplayResolution(this string value)
        {

            int componentsCount = 2;

            bool isDataValid = false;

            if(String.IsNullOrEmpty(value))
            {
                return null;
            }

            var values = value.Split(new string[] {"x"}, StringSplitOptions.RemoveEmptyEntries);

            if(values.Length != componentsCount)
            {
                return null;
            }

            int width, height;

            isDataValid = Int32.TryParse(values[0], out width);

            isDataValid = Int32.TryParse(values[1], out height);

            if(!isDataValid)
            {
                return null;
            }

            return new DisplayResolution { Width = width, Height = height };
        }
    }
}
