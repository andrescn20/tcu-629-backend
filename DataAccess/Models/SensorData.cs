using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class SensorData
    {
        [Key]
        public int SensorId { get; set; }
        public string BoardId { get; set; }
        public string CalentadorId { get; set; }
        public decimal Temperature { get; set; }
        public decimal Humidity { get; set; }
        public DateTime Timestamp { get; set; }

    }
}
