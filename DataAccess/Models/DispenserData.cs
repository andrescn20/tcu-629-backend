using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class DispenserData
    {
        [Key]
        public int Id { get; set; }
        public int SensorId { get; set; }
        public DateTime Timestamp { get; set; }

        //Navigation Properties
        public Sensor Sensor { get; set; }
    }
}
