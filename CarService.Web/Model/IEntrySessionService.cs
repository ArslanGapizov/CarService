using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Web.Model
{
    public interface IEntrySessionService
    {
        int GetSession();
        void RemoveSession(int entryNumber);
        void SetVehicle(int entryNumber, string modelName, string makeName, int year, int mileage);
        VehicleDTO GetVehicle(int entryNumber);
        void SetServiceNeeds(int entryNumber, List<int> serviceNeedIds, string text);
        List<int> GetServiceNeeds(int entryNumber);
        void setDateTime(int entryNumber, DateTimeDTO data);
        DateTime GetDateTime(int entryNumber);
    }
}
