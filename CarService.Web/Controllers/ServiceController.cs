using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarService.Domain.Repositories;
using CarService.Web.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Service")]
    public class ServiceController : Controller
    {
        IVehicleRepository _vehicalRepository;
        IEntrySessionService _entryService;
        public ServiceController(IVehicleRepository vehicalRepository,
                                 IEntrySessionService entryService)
        {
            _vehicalRepository = vehicalRepository;
            _entryService = entryService;
        }

        [Route("session")]
        public async Task<IActionResult> GetSession()
        {
            var session = new { token = _entryService.GetSession() };

            return Ok(session);
        }

        [Route("years")]
        public async Task<IActionResult> GetVehicleYear()
        {
            List<int> list = new List<int>();

            for (int i = 1970; i <= DateTime.Now.Year; i++)
            {
                list.Add(i);
            }

            return Ok(list);
        }

        [Route("makes")]
        public async Task<IActionResult> GetVehicleMake(int year)
        {
            var data = _vehicalRepository
                .GetVehicleMakesByYear(year);

            var result = new List<VehicleMakeDTO>();
            foreach (var entry in data)
            {
                result.Add(new VehicleMakeDTO
                {
                    Name = entry.Name,
                    Id = entry.VehicleMakeId
                });
            }
            return Ok(result);
        }

        [Route("models")]
        public async Task<IActionResult> GetVehicleModel(string name, int year)
        {

            var data = _vehicalRepository
                .GetVehicleModelsByVehicleMakeNameAndYear(name, year);

            var result = new List<VehicleModelDTO>();

            foreach (var entry in data)
            {
                result.Add(new VehicleModelDTO
                {
                    Id = entry.VehicleModelId,
                    Name = entry.Name,
                    StartYear = entry.StartYear,
                    EndYear = entry.EndYear
                });
            }

            return Ok(result);
        }

        public async Task<IActionResult> PostVehicle()
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> GetServiceNeeds()
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> PostServiceNeeds()
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> GetDateAndTime()
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> PostDateAndTime()
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> GetSummary()
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> PostContactInformation()
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> GetAppointmentInformation()
        {
            throw new NotImplementedException();
        }
    }
}