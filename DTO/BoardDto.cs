namespace DTO
{
    public class BoardDto
    {
        public int BoardId { get; set; }
        public string Microcontroller { get; set; }
        public string? Description { get; set; }
        public bool IsInstalled { get; set; }
        public string BoardSerial { get; set; }
        public List<SensorDto>? Sensors { get; set; }


    }
}
