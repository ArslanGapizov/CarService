using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Web.Model
{
    public class VehicleDTO
    {
        public string Model { get; set; }
        public string Make { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
    }
}
