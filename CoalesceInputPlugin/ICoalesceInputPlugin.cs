using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoalesceInputPlugin
{
    public interface ICoalesceInputPlugin
    {
        PlugInDetails GetDetails();
        List<SensorDetails> GetSensors();
        List<SensorReadingPackage> GetReadings();
        void Initialise();
        void ShowConfig();
        void ShowMonitor();
    }

    public class PlugInDetails
    {
        public Guid Id { get; set; }
        public string ShortName { get; set; }
        public string Author { get; set; }
        public string SupportLink { get; set; }
        public double Version { get; set; }
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

    public class OrientationalReading
    {
        public double Pitch { get; set; }
        public double Yaw { get; set; }
        public double Roll { get; set; }
    }

    public class PositionalReading
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }

    public enum DestinationJoints
    {
        Head,
        Neck,
        Shoulder_R,
        Shoulder_L,
        Elbow_R,
        Elbow_L,
        Wrist_R,
        Wrist_L,
        Hand_R,
        Hand_L,
        Spine,
        Hip_R,
        Hip_L,
        Knee_R,
        Knee_L,
        Ankle_R,
        Ankle_L,
        Foot_R,
        Foot_L,
    }

    public interface IModifier
    {
        
    }
}
