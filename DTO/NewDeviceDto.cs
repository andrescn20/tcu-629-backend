namespace DTO
{
    public class NewDeviceDto
    
    {
        public int DeviceId { get; set; }
        public int DeviceTypeId { get; set; }
        public string? Description { get; set; }
        public string Location { get; set; }
        public DateTime? Added_at { get; set; }

        //Navigation Properties
        public int BoardId { get; set; }
        public List<int> SensorIds { get; set; }
    }
}