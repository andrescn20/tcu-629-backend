using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace DTO
{
    public class TemperatureDataDto
    {
        public int Id { get; set; }
        public decimal Temperature { get; set; }
        public DateTime Timestamp { get; set; }
        public string SensorAddress { get; set; } 
        public int? SensorId { get; set; }
        public int? BoardId { get; set; }
        public int? DeviceId { get; set; }
    }
}
