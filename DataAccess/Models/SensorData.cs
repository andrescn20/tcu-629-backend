using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class SensorData
    {
        [Key]
        public int Id { get; set; }
        public int SensorId { get; set; }
        public string BoardId { get; set; }
        public decimal Temperature { get; set; }
        public DateTime Timestamp { get; set; }

    }
}
