using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class DispenserLevelData
    {
        [Key]
        public int Id { get; set; }
        public int SensorId { get; set; }
        public bool LiquidLevel { get; set; }
        public DateTime Timestamp { get; set; }

        public Sensor Sensor { get; set; }
    }
}
