using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
        IServiceRepository _serviceRepository;
        IEntrySessionService _entryService;
        public ServiceController(IVehicleRepository vehicalRepository,
                                 IEntrySessionService entryService,
                                 IServiceRepository serviceRepository)
        {
            _vehicalRepository = vehicalRepository;
            _entryService = entryService;
            _serviceRepository = serviceRepository;
        }
        
        [HttpGet("session")]
        public async Task<IActionResult> GetSession()
        {
            var session = new { token = _entryService.GetSession() };

            return Ok(session);
        }
        
        [HttpGet("years")]
        public async Task<IActionResult> GetVehicleYear()
        {
            List<int> list = new List<int>();

            for (int i = 1970; i <= DateTime.Now.Year; i++)
            {
                list.Add(i);
            }

            return Ok(list);
        }
        
        [HttpGet("makes")]
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
        
        [HttpGet("models")]
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
        [HttpPost("vehicle")]
        public IActionResult PostVehicle(string session, [FromBody] VehicleDTO data)
        {
            _entryService.SetVehicle(int.Parse(session), data.Model, data.Make, data.Year, data.Mileage);
            return Ok();
        }
        [HttpGet("needs")]
        public async Task<IActionResult> GetServiceNeeds()
        {
            var data = _serviceRepository.Get();
            List<VehicleServiceDTO> result = new List<VehicleServiceDTO>();
            foreach (var entry in data)
            {
                result.Add(new VehicleServiceDTO
                {
                    Id = entry.ServiceId,
                    Name = entry.Name
                });
            }
            return Ok(result);
        }
        [HttpPost("needs")]
        public async Task<IActionResult> PostServiceNeeds(string session, [FromBody]ServiceNeedsDTO data)
        {
            _entryService.SetServiceNeeds(int.Parse(session), data.Ids.ToList(), data.Text);
            return Ok();
        }
        [HttpGet("time")]
        public async Task<IActionResult> GetDateAndTime(int year, int month, int day)
        {
            List<TimeDTO> list = new List<TimeDTO>();
            for(int i = 10; i < 20; i++)
            {
                list.Add(new TimeDTO
                {
                    Hours = i,
                    Minutes = 0,
                    Available = true
                });
            }
            return Ok(list);
        }
        [HttpPost("time")]
        public async Task<IActionResult> PostDateAndTime(string session, [FromBody]DateTimeDTO data)
        {
            _entryService.setDateTime(int.Parse(session), data);
            return Ok();
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary(string session)
        {
            SummaryDTO result = new SummaryDTO();

            result.Vehicle = _entryService.GetVehicle(int.Parse(session));

            result.DateTime = _entryService.GetDateTime(int.Parse(session));

            List<string> services = new List<string>();

            var data = _serviceRepository.Get();
            var ids = _entryService.GetServiceNeeds(int.Parse(session));
            foreach (var id in ids)
            {
                services.Add(data.Find(el => el.ServiceId == id).Name);
            }
            result.Services = services;

            return Ok(result);
        }
        [HttpPost("contactInfo")]
        public async Task<IActionResult> PostContactInformation(string session, [FromBody]ContactInfoDTO data)
        {

            var vehicle = _entryService.GetVehicle(int.Parse(session));
            var dateTime = _entryService.GetDateTime(int.Parse(session));
            List<string> services = new List<string>();

            var allServices = _serviceRepository.Get();
            var ids = _entryService.GetServiceNeeds(int.Parse(session));
            foreach (var id in ids)
            {
                services.Add(allServices.Find(el => el.ServiceId == id).Name);
            }
            var fromAddress = new MailAddress("allrestaurants.max@gmail.com", "CarService"); //email
            
            var toAddress = new MailAddress(data.Email, "CarService");
            //const string fromPassword = "vasyanomer1"; //Password
            const string fromPassword = "Project777"; //Password
            //const string fromPassword = "@1Priv@t&123$"; //Password
            string subject = "Your appointment has been scheduled";
            string body = "<h2> Hello, " + data.FirstName + " " + data.LastName + "<h2>"
                          +"<p>"+ "Your appointment has been scheduled" + "</p>"
                          + "<h3>Date and Time: </h3>"
                          + "<p>" +dateTime+ "</p>"
                          + "<h3>Vehicle: </h3>"
                          + "<p>" + $"{vehicle.Year} {vehicle.Make} {vehicle.Model}" + "</p>"
                          + "<h3>" + "Services:" + "</h3>"
                          +"<ul>";
            foreach(var service in services)
            {
                body += "<li>" + service+"</li>";
            }
            body += "</ul>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                //UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }
            //_entryService.RemoveSession(int.Parse(session));
            return Ok(new { dateTime = dateTime});
        }

        public async Task<IActionResult> GetAppointmentInformation()
        {
            throw new NotImplementedException();
        }
    }
}