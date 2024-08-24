namespace DTO
{
    public class SensorDto
    {
        public int SensorId { get; set; }
        public string SensorName { get; set; }
        public string Description { get; set; }
        public int SensorTypeId { get; set; }
        public bool IsAvailable { get; set; }
        public string? SensorType { get; set; }
        public string SensorAddress { get; set; }
    }
    public class SensorTypeDto
    
    {
        public int TypeId { get; set; }
        public string Type { get; set; }
    }
}
