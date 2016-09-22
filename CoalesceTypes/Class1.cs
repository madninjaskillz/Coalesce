using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoalesceTypes
{
    public class JointPackage
    {
        public BodyJoints Joint { get; set; }
        public bool[] Triggers { get; set; } = new bool[3];
        public OrientationalReading Orientation { get; set; } = new OrientationalReading();
        public PositionalReading Position { get; set; } = new PositionalReading();
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


    public enum BodyJoints
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
}
