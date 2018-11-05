using CarService.Domain.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarService.Domain.Repositories
{
    public interface IVehicleRepository
    {
        List<VehicleMake> GetVehicleMakesByYear(int year);
        List<VehicleModel> GetVehicleModelsByVehicleMakeNameAndYear(string name, int year);
    }
}
