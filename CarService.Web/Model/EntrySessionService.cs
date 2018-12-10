using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Web.Model
{
    public class EntrySessionService: IEntrySessionService
    {
        private int counter = 20000;
        private readonly Dictionary<int, Entry> _items;

        public EntrySessionService()
        {
            _items = new Dictionary<int, Entry>();
        }

        public int GetSession()
        {
            ++counter;
            _items[counter] = new Entry();
            return counter;
        }

        public void SetVehicle(int entryNumber, string modelName, string makeName, int year, int mileage)
        {
            if (!_items.ContainsKey(entryNumber))
                return;

            Entry entry = _items[entryNumber];
            entry.VehicleMakeName = makeName;
            entry.VehicleModelName = modelName;
            entry.VehicleYear = year;
            entry.Mileage = mileage;
        }
        public VehicleDTO GetVehicle(int entryNumber)
        {
            if (!_items.ContainsKey(entryNumber))
                return null;

            Entry entry = _items[entryNumber];
            VehicleDTO result = new VehicleDTO
            {
                Make = entry.VehicleMakeName,
                Model = entry.VehicleModelName,
                Year = entry.VehicleYear,
                Mileage = entry.Mileage
            };

            return result;
        }
        public void SetServiceNeeds(int entryNumber, List<int> serviceNeedIds, string text)
        {
            if (!_items.ContainsKey(entryNumber))
                return;

            Entry entry = _items[entryNumber];
            entry.ServiceNeedIds = serviceNeedIds;
            entry.ServiceNeedText = text;
        }
        public List<int> GetServiceNeeds(int entryNumber)
        {
            if (!_items.ContainsKey(entryNumber))
                return null;

            Entry entry = _items[entryNumber];
            return entry.ServiceNeedIds;


        }
        public void setDateTime(int entryNumber, DateTimeDTO data)
        {
            if (!_items.ContainsKey(entryNumber))
                return;

            Entry entry = _items[entryNumber];
            entry.DateTime = data;
        }
        public DateTime GetDateTime(int entryNumber)
        {
            if (!_items.ContainsKey(entryNumber))
                return new DateTime();

            Entry entry = _items[entryNumber];

            DateTimeDTO dtDTO = entry.DateTime;
            DateTime result = new DateTime(dtDTO.Year,
                                           dtDTO.Month,
                                           dtDTO.Day,
                                           dtDTO.Hours,
                                           dtDTO.Minutes,
                                           0);
            return result;
        }

        public void RemoveSession(int entryNumber)
        {
            if (!_items.ContainsKey(entryNumber))
                return;
            _items.Remove(entryNumber);
        }
    }
}
