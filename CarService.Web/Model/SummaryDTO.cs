using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Web.Model
{
    public class SummaryDTO
    {
        public DateTime DateTime { get; set; }
        public VehicleDTO Vehicle { get; set; }
        public List<string> Services { get; set; }
    }
}
