using CarService.Domain.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarService.Domain.Repositories
{
    public class VehicleRepository: IVehicleRepository, IDisposable
    {
        private CarServiceDbContext _ctx;
        public VehicleRepository(CarServiceDbContext context)
        {
            _ctx = context;
        }

        public List<VehicleMake> GetVehicleMakesByYear(int year)
        {
            List<VehicleMake> vehicleMakes = _ctx.VehicleMakes
                .Where(x => x.VehicleModels.Where(y => y.StartYear <= year && y.EndYear >= year).Count() != 0)
                .ToList();

            return vehicleMakes;
        }

        public List<VehicleModel> GetVehicleModelsByVehicleMakeNameAndYear(string name, int year)
        {
            List<VehicleModel> vehicalModels = _ctx.VehicleModels
                .Where(x => x.VehicleMake.Name == name && x.StartYear <= year && x.EndYear >= year)
                .ToList();

            return vehicalModels;
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _ctx.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
