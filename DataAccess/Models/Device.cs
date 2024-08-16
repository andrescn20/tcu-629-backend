using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Device
    {
        [Key]
        public int DeviceId { get; set; }
        public int DeviceTypeId { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime Added_at { get; set; }

        //Navigation Properties
        public ICollection<Board> Boards { get; set; }
        public DeviceType DeviceType { get; set; }
    }
}
