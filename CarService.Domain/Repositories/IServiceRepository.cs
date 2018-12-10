using CarService.Domain.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarService.Domain.Repositories
{
    public interface IServiceRepository
    {
        List<Service> Get();
    }
}
