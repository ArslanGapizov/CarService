using CarService.Domain.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Web.Model
{
    public class DbInitializer
    {
        internal static void Seed(IApplicationBuilder app)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            using (CarServiceDbContext ctx
                = scope.ServiceProvider.GetRequiredService<CarServiceDbContext>())
            {
                ctx.Database.EnsureDeleted();
                ctx.Database.EnsureCreated();

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
                    ctx.Add(entry);
                }

                List<Service> services = new List<Service>
                {
                    new Service
                    {
                        Name = "Oil & Filter Change'"
                    },
                    new Service
                    {
                        Name = "Wiper Blades Change"
                    },

                    new Service
                    {
                        Name = "Brake Issues"
                    },
                    new Service
                    {
                        Name = "Wheels Alignment"
                    },
                    new Service
                    {
                        Name = "Air Filter Change"
                    },
                    new Service
                    {
                        Name = "Battery Change"
                    },
                    new Service
                    {
                        Name = "Tire Rotation"
                    },
                    new Service
                    {
                        Name = "Check Engine Light"
                    }
                };
                foreach (var entry in services)
                {
                    ctx.Add(entry);
                }
                ctx.SaveChanges();
            }
        }
    }
}
