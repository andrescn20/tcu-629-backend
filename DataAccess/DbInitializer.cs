using DataAccess;
using DataAccess.Models;

public static class DbInitializer
{
    public static void Seed(MonitoringDbContext context)
    {
        if (!context.DeviceTypes.Any())
        {
            context.DeviceTypes.AddRange(
                new DeviceType
                {
                    Type = "Calentador Solar de Agua"
                },
                new DeviceType
                {
                    Type = "Dispensador Automático de Alcohol"
                }
            );

            context.SaveChanges();
        }

        if (!context.Devices.Any())
        {
            context.Devices.AddRange(
                new Device
                {
                    DeviceTypeId = 1,  
                    Description = "Temperature monitoring device for living room",
                    Location = "Living Room",
                    Added_at = DateTime.Now
                },
                new Device
                {
                    DeviceTypeId = 2,  
                    Description = "Humidity monitoring device for greenhouse",
                    Location = "Greenhouse",
                    Added_at = DateTime.Now
                }
            );

            context.SaveChanges();
        }

        if (!context.Boards.Any())
        {
            context.Boards.AddRange(
                new Board
                {
                    DeviceId = 3,  // Ensure this matches an existing DeviceId
                    Microcontroller = "ESP32",
                    Description = "Main board for temperature monitoring",
                    BoardSerial = "Esp32001",
                    IsInstalled = false,
                },
                new Board
                {
                    DeviceId = 4,  // Ensure this matches an existing DeviceId
                    Microcontroller = "Arduino Nano",
                    Description = "Backup board for humidity sensors",
                    BoardSerial = "Esp32002",
                    IsInstalled = true,

                },
                new Board
                {
                    DeviceId = 4,  // Ensure this matches an existing DeviceId
                    Microcontroller = "Raspberry Pi Pico",
                    Description = "Experimental board for IoT projects",
                    BoardSerial = "Esp32003",
                    IsInstalled = true,
                }
            );
            context.SaveChanges();
        }

        if (!context.SensorTypes.Any())
        {
            context.SensorTypes.AddRange(
                new SensorType
                {
                    Type = "Temperature"
                },
                new SensorType
                {
                    Type = "Liquid Level"
                },
                new SensorType
                {
                    Type = "Ultrasonic"
                }
            );

            context.SaveChanges();
        }

        if (!context.Sensors.Any())
        {
            context.Sensors.AddRange(
                new Sensor
                {
                    SensorName = "Living Room Temperature Sensor",
                    Description = "Monitors the temperature in the living room",
                    BoardId = 8,  
                    SensorTypeId = 1,
                    SensorAddress = "28df0079971303dd",
                    IsAvailable = true,
                },
                new Sensor
                {
                    SensorName = "Greenhouse Humidity Sensor",
                    Description = "Liquid Level",
                    BoardId = null,
                    SensorTypeId = 2, 
                    SensorAddress = "28df0079971303aa",
                    IsAvailable = false,

                },
                new Sensor
                {
                    SensorName = "Kitchen Temperature Sensor",
                    Description = "Monitors the temperature in the kitchen",
                    BoardId = 8, 
                    SensorTypeId = 1,
                    SensorAddress = "28df0079971303bb",
                    IsAvailable = true,

                },
                new Sensor
                {
 
                    SensorName = "Garage Temperature Sensor",
                    Description = "Monitors the temperature in the garage",
                    BoardId = null, 
                    SensorTypeId = 1,
                    SensorAddress = "28df0079971303cc",
                    IsAvailable = false,

                },
                new Sensor
                {
                    SensorName = "Office Temperature Sensor",
                    Description = "Ultrasonic",
                    BoardId = null,
                    SensorTypeId = 3 ,
                    SensorAddress = "28df0079971303ee",
                    IsAvailable = false,

                }
            );

            context.SaveChanges();
        }
        
        if (!context.TemperatureData.Any())
        {
            var random = new Random();
            var temperatureDataList = new List<TemperatureData>();

            for (int i = 1; i <= 50; i++)
            {
                temperatureDataList.Add(new TemperatureData
                {
                    Temperature = Math.Round((decimal)(random.NextDouble() * 40), 2), 
                    Timestamp = DateTime.Now.AddMinutes(-i * 5), 
                    SensorId = 46 
                });
            }

            for (int i = 1; i <= 76; i++)
            {
                temperatureDataList.Add(new TemperatureData
                {
                    Temperature = Math.Round((decimal)(random.NextDouble() * 40), 2),
                    Timestamp = DateTime.Now.AddMinutes(-i * 5),
                    SensorId = 50
                });
            }

            context.TemperatureData.AddRange(temperatureDataList);
            context.SaveChanges();
        }

        //if (!context.DispenserLevelData.Any())
        //{
        //    var random = new Random();
        //    var dispenserLevelDataList = new List<DispenserLevelData>();

        //    for (int i = 1; i <= 15; i++)
        //    {
        //        dispenserLevelDataList.Add(new DispenserLevelData
        //        {                    SensorId = random.Next(41, 44), 
        //            LiquidLevel = random.Next(0, 2) == 1, 
        //            Timestamp = DateTime.Now.AddMinutes(-i * 10) 
        //        });
        //    }

        //    context.DispenserLevelData.AddRange(dispenserLevelDataList);
        //    context.SaveChanges();
        //}

        //if (!context.DispenserData.Any())
        //{
        //    var random = new Random();
        //    var dispenserDataList = new List<DispenserData>();

        //    for (int i = 1; i <= 50; i++)
        //    {
        //        dispenserDataList.Add(new DispenserData
        //        {
        //            SensorId = 41, 
        //            Timestamp = DateTime.Now.AddMinutes(-i * 5) 
        //        });
        //    }

        //    context.DispenserData.AddRange(dispenserDataList);
        //    context.SaveChanges();
        //}
    }
}
