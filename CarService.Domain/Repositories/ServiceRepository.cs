using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarService.Domain.Database;

namespace CarService.Domain.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private CarServiceDbContext _ctx;
        public ServiceRepository(CarServiceDbContext context)
        {
            _ctx = context;
        }
        public List<Service> Get()
        {
            return _ctx.Services.ToList();
        }

    }
}
