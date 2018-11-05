using CarService.Domain.Database;
using CarService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;

namespace CarService.Tests
{
    [TestClass]
    public class VehicleRepositoryTest
    {
        CarServiceDbContext _dbctx;
        VehicleRepository _repo;
        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<CarServiceDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDb")
                .Options;
            _dbctx = new CarServiceDbContext(options);
            _repo = new VehicleRepository(_dbctx);

        }

        private void ClearAndAddData()
        {
            _dbctx.VehicleMakes.RemoveRange(_dbctx.VehicleMakes);
            _dbctx.VehicleModels.RemoveRange(_dbctx.VehicleModels);
            _dbctx.SaveChangesAsync();

            List<VehicleMake> data = new List<VehicleMake>
            {
                new VehicleMake { VehicleModels = new List<VehicleModel> {
                                      new VehicleModel {
                                          Name = "Outlander",
                                          EndYear = 2018,
                                          StartYear = 2011

                                      },
                                      new VehicleModel {
                                          Name = "Lancer",
                                          EndYear = 2018,
                                          StartYear = 2008

                                      },
                                      new VehicleModel {
                                          Name = "Space Star",
                                          EndYear = 2011,
                                          StartYear = 1995

                                      },
                                  },
                                  Name = "Mitsubishi",
                },
                new VehicleMake { VehicleModels = new List<VehicleModel> {
                                      new VehicleModel {
                                          Name = "X5",
                                          EndYear = 2018,
                                          StartYear = 2010

                                      },
                                      new VehicleModel {
                                          Name = "X3",
                                          EndYear = 2018,
                                          StartYear = 2015

                                      },
                                      new VehicleModel {
                                          Name = "6",
                                          EndYear = 2009,
                                          StartYear = 1995

                                      },
                                  },
                                  Name = "BMW",
                },
                new VehicleMake { VehicleModels = new List<VehicleModel> {
                                      new VehicleModel {
                                          Name = "Cayenne",
                                          EndYear = 2018,
                                          StartYear = 2005

                                      },
                                      new VehicleModel {
                                          Name = "Panamera",
                                          EndYear = 2018,
                                          StartYear = 2010

                                      },
                                      new VehicleModel {
                                          Name = "Cayman",
                                          EndYear = 2015,
                                          StartYear = 1990

                                      },
                                  },
                                  Name = "Porsche",
                },
            };
            foreach (var entry in data)
            {
                _dbctx.Add(entry);
            }
            _dbctx.SaveChangesAsync();
        }

        [TestMethod]
        public void TestGetVehicleMakes()
        {
            ClearAndAddData();

            List<VehicleMake> result = _repo.GetVehicleMakesByYear(1990);
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0].Name, "Porsche");


            result = _repo.GetVehicleMakesByYear(1970);
            Assert.AreEqual(result.Count, 0);

            result = _repo.GetVehicleMakesByYear(2018);
            Assert.AreEqual(result.Count, 3);

            Debug.WriteLine(result.Count);
        }
        [TestMethod]
        public void TestGetVehicleModel()
        {
            ClearAndAddData();

            List<VehicleModel> result = _repo.GetVehicleModelsByVehicleMakeNameAndYear("Porsche", 2018);

            Assert.AreEqual(result.Count, 2);

            result = _repo.GetVehicleModelsByVehicleMakeNameAndYear("Porsche", 1990);

            Assert.AreEqual(result[0].Name, "Cayman");
        }
    }
}
