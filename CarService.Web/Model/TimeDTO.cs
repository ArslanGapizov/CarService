using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Web.Model
{
    public class TimeDTO
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public bool Available { get; set; }
        public override string ToString()
        {
            return String.Format(
                "{0:00}:{1:00}",
                this.Hours, this.Minutes);
        }
    }
}
