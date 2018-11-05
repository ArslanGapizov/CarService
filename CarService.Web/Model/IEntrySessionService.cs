using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Web.Model
{
    public interface IEntrySessionService
    {
        int GetSession();
        void SetVehicle(int entryNumber, string modelName, string makeName, int year, int mileage);
    }
}
