using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class DeviceType
    {
        [Key]
        public int DeviceTypeId { get; set; }
        public string Type { get; set; }

        //Navigation properties
        public ICollection<Device> Devices { get; set; }

    }
}
