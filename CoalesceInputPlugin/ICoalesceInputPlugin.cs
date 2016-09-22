using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoalesceTypes;

namespace CoalesceInputPlugin
{
    public interface ICoalesceInputPlugin: ICoalescePlugin
    {
        
        List<SensorDetails> GetSensors();
        List<SensorReadingPackage> GetReadings();
        void Initialise();
        void ShowConfig();
        void ShowMonitor();

        string GetConfig();
        void SetConfig(string config);
    }

    public class SensorDetails
    {
        public Guid Parent { get; set; }
        public int Id { get; set; }
        public string SensorName { get; set; }
        public bool ProvidesPosition { get; set; }
        public bool ProvidesOrientation { get; set; }
    }

    public class SensorReadingPackage
    {
        public int Id { get; set; }
        public OrientationalReading Orientation { get; set; }
        public PositionalReading Position { get; set; }
    }


}
