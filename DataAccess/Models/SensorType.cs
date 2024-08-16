using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class SensorType
    {
        [Key]
        public int SensorTypeId { get; set; }
        public string Type { get; set; }

        //Navigation properties
        public ICollection<Sensor> Sensors { get; set; }

    }
}
