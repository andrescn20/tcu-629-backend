namespace DTO
{
    public class DeviceDto
    {
        public int DeviceId { get; set; }
        public int DeviceTypeId { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime Added_at { get; set; }
        public string? DeviceType { get; set; }
    }
    public class DeviceTypeDto
    
    {
        public int TypeId { get; set; }
        public string Type { get; set; }

        public ICollection<DeviceDto>? Devices { get; set; }
    }
}