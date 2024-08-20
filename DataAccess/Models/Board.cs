using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Board
    {
        [Key]
        public int BoardId { get; set; }
        public int DeviceId { get; set; }
        public string Microcontroller { get; set; }
        public string? Description { get; set; }

        //Navigation Properties
        public Device Device { get; set; }
        public ICollection<Sensor> Sensors { get; set; }
    }
}
