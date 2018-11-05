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
    }
}
