using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTOs.Devices
{
    public class DeviceDto
    {
        public int DeviceId { get; set; }
        public int DeviceTypeId { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime Added_at { get; set; }
    }
    public class DeviceTypeDto
    
    {
        public int DeviceTypeId { get; set; }
        public string Type { get; set; }

        public ICollection<DeviceDto>? Devices { get; set; }
    }
}
