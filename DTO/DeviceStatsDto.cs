namespace DTO
{
    public class DeviceStatsDto
    {
        public int DeviceId { get; set; }

        public double MaxTemperature { get; set; }
        public DateTime MaxTemperatureTime { get; set; }

        public double MinTemperature { get; set; }
        public DateTime MinTemperatureTime { get; set; }

        public double LatestTemperature { get; set; }
        public DateTime LatestTemperatureTime { get; set; }

        public double MedianTemperature { get; set; }
    }
}
