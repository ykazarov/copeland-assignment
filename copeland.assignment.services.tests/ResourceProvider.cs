using copeland.assignment.models;

namespace copeland.assignment.services.tests;

public static class ResourceProvider
{
    public static DeviceDataFoo1Model GetFoo1()
    {
        return new DeviceDataFoo1Model()
        {
            PartnerId = 1,
            PartnerName = "Partner 1",
            Trackers = new List<Tracker>()
            {
                new Tracker()
                {
                    Id = 1,
                    Model = "Model 1",
                    ShipmentStartDtm = DateTime.Parse("01/01/2024"),
                    Sensors = new List<Sensor>()
                    {
                        new Sensor()
                        {
                            Id = 1,
                            Name = "Temperature",
                            Crumbs = new List<Crumb>()
                            {
                                new Crumb()
                                {
                                    CreatedDtm = DateTime.Parse("01/12/2024"),
                                    Value = 1.0
                                }
                            }
                        }
                    }
                },
                new Tracker()
                {
                    Id = 1,
                    Model = "Model 2",
                    ShipmentStartDtm = DateTime.Now,
                    Sensors = new List<Sensor>()
                    {
                        new Sensor()
                        {
                            Id = 1,
                            Name = "Temperature",
                            Crumbs = new List<Crumb>()
                            {
                                new Crumb()
                                {
                                    CreatedDtm = DateTime.Now,
                                    Value = 2.0
                                }
                            }
                        }
                    }
                }
            }
        };
    }

    public static DeviceDataFoo2Model GetFoo2()
    {
        return new DeviceDataFoo2Model()
        {
            Company = "Company 1",
            CompanyId = 1,
            Devices = new List<Device>()
            {
                new Device()
                {
                    DeviceID = 1,
                    Name = "Device 1",
                    StartDateTime = DateTime.Now.AddDays(-1),
                    SensorData = new List<SensorDatum>()
                    {
                        new SensorDatum()
                        {
                            DateTime = DateTime.Parse("01/15/2024"),
                            SensorType = "TEMP",
                            Value = 3.0
                        }
                    }
                },
                new Device()
                {
                    DeviceID = 2,
                    Name = "Device 2",
                    StartDateTime = DateTime.Parse("01/01/2024"),
                    SensorData = new List<SensorDatum>()
                    {
                        new SensorDatum()
                        {
                            DateTime = DateTime.Now,
                            SensorType = "TEMP",
                            Value = 4.0
                        }
                    }
                }
            }
        };
    }
}
