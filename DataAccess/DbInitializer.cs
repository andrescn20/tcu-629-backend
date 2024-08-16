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
                    DeviceTypeId = 1,  // Example: 1 could represent a specific type of device
                    Description = "Temperature monitoring device for living room",
                    Location = "Living Room",
                    Added_at = DateTime.Now
                },
                new Device
                {
                    DeviceTypeId = 2,  // Example: 2 could represent a different type of device
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
                },
                new Board
                {
                    DeviceId = 4,  // Ensure this matches an existing DeviceId
                    Microcontroller = "Arduino Nano",
                    Description = "Backup board for humidity sensors",
                },
                new Board
                {
                    DeviceId = 4,  // Ensure this matches an existing DeviceId
                    Microcontroller = "Raspberry Pi Pico",
                    Description = "Experimental board for IoT projects",
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
                    BoardId = 8,  // Linked to an existing Board with BoardId 1
                    SensorTypeId = 1  // Replace with an existing SensorTypeId
                },
                new Sensor
                {
                    SensorName = "Greenhouse Humidity Sensor",
                    Description = "Liquid Level",
                    BoardId = 10,  // Linked to an existing Board with BoardId 2
                    SensorTypeId = 2  // Replace with an existing SensorTypeId
                },
                new Sensor
                {
                    SensorName = "Kitchen Temperature Sensor",
                    Description = "Monitors the temperature in the kitchen",
                    BoardId = 8,  // Linked to an existing Board with BoardId 1
                    SensorTypeId = 1  // Replace with an existing SensorTypeId
                },
                new Sensor
                {
 
                    SensorName = "Garage Temperature Sensor",
                    Description = "Monitors the temperature in the garage",
                    BoardId = 9,  // Linked to an existing Board with BoardId 3
                    SensorTypeId = 1  // Replace with an existing SensorTypeId
                },
                new Sensor
                {
                    SensorName = "Office Temperature Sensor",
                    Description = "Ultrasonic",
                    BoardId = 10,  // Linked to an existing Board with BoardId 3
                    SensorTypeId = 3  // Replace with an existing SensorTypeId
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
                    Temperature = Math.Round((decimal)(random.NextDouble() * 40), 2), // Random temperature between 0 and 40°C
                    Timestamp = DateTime.Now.AddMinutes(-i * 5), // Spacing out timestamps every 5 minutes
                    SensorId = random.Next(10, 14) // Assuming you have SensorId 1 and 2 seeded already
                });
            }

            context.TemperatureData.AddRange(temperatureDataList);
            context.SaveChanges();
        }

        if (!context.DispenserLevelData.Any())
        {
            var random = new Random();
            var dispenserLevelDataList = new List<DispenserLevelData>();

            for (int i = 1; i <= 15; i++)
            {
                dispenserLevelDataList.Add(new DispenserLevelData
                {                    SensorId = random.Next(10, 14), // Assuming SensorId values range from 1 to 5
                    LiquidLevel = random.Next(0, 2) == 1, // Random boolean value
                    Timestamp = DateTime.Now.AddMinutes(-i * 10) // Spacing out timestamps every 10 minutes
                });
            }

            context.DispenserLevelData.AddRange(dispenserLevelDataList);
            context.SaveChanges();
        }

        if (!context.DispenserData.Any())
        {
            var random = new Random();
            var dispenserDataList = new List<DispenserData>();

            for (int i = 1; i <= 50; i++)
            {
                dispenserDataList.Add(new DispenserData
                {
                    SensorId = 14, // Assuming SensorId values range from 1 to 5
                    Timestamp = DateTime.Now.AddMinutes(-i * 5) // Spacing out timestamps every 5 minutes
                });
            }

            context.DispenserData.AddRange(dispenserDataList);
            context.SaveChanges();
        }
    }
}
