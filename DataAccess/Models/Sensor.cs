using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Sensor
    {
        [Key]
        public int SensorId { get; set; }
        public string SensorName { get; set;  }
        public string Description { get; set; }
        public int BoardId { get; set; }
        public int SensorTypeId { get; set; }

        //Navigation Properties
        public SensorType SensorType { get; set; }
        public Board Board { get; set; }
        public ICollection<TemperatureData> TemperatureDataList { get; set; }
        public ICollection<DispenserData> DispenserDataList { get; set; }
        public ICollection<DispenserLevelData> DispenserLevelDataList { get; set; }



    }
}
