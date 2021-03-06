﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Web.Model
{
    public class Entry
    {
        public int VehicleYear { get; set; }
        public string VehicleMakeName { get; set; }
        public string VehicleModelName { get; set; }
        public int Mileage { get; set; }
        public List<int> ServiceNeedIds { get; set; } = new List<int>();
        public string ServiceNeedText { get; set; }
        public DateTimeDTO DateTime { get; set; }
    }
}
