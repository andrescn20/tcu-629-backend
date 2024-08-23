using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class TemperatureData
    {
        [Key]
        public int Id { get; set; }
        public decimal Temperature { get; set; }
        public DateTime Timestamp { get; set; }
        public int SensorId { get; set; }

        //Navigation Properties
        public Sensor Sensor { get; set; }
    }
}
