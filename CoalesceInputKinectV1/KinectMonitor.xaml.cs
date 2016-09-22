using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CoalesceInputPlugin;
using Microsoft.Kinect;

namespace coalesce
{
    /// <summary>
    /// Interaction logic for KinectMonitor.xaml
    /// </summary>
    public partial class KinectMonitor : Window
    {
        private const float RenderWidth = 640.0f;
        private const float RenderHeight = 480.0f;
        private const double JointThickness = 3;
        private const double BodyCenterThickness = 10;
        private const double ClipBoundsThickness = 10;
        private readonly Brush centerPointBrush = Brushes.Blue;
        private readonly Brush trackedJointBrush = new SolidColorBrush(Color.FromArgb(255, 68, 192, 68));
        private readonly Brush inferredJointBrush = Brushes.Yellow;
        private readonly Pen trackedBonePen = new Pen(Brushes.Green, 6);
        private readonly Pen inferredBonePen = new Pen(Brushes.Gray, 1);
        public DrawingGroup drawingGroup;
        private DrawingImage imageSource;

        public KinectMonitor()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            this.drawingGroup = new DrawingGroup();
            this.imageSource = new DrawingImage(this.drawingGroup);
            Image.Source = this.imageSource;
        }
        
        public void DoSimpleDraw(List<SensorReadingPackage> readings, List<SensorDetails> sensors)
        {
            using (DrawingContext dc = drawingGroup.Open())
            {
                DrawJointToJoint(dc, readings, sensors, JointType.Head, JointType.ShoulderCenter);
                DrawJointToJoint(dc, readings, sensors, JointType.ShoulderCenter, JointType.ShoulderLeft);
                DrawJointToJoint(dc, readings, sensors, JointType.ShoulderCenter, JointType.ShoulderRight);
                DrawJointToJoint(dc, readings, sensors, JointType.ShoulderCenter, JointType.Spine);
                DrawJointToJoint(dc, readings, sensors, JointType.Spine, JointType.HipCenter);
                DrawJointToJoint(dc, readings, sensors, JointType.HipCenter, JointType.HipLeft);
                DrawJointToJoint(dc, readings, sensors, JointType.HipCenter, JointType.HipRight);

                DrawJointToJoint(dc, readings, sensors, JointType.ShoulderLeft, JointType.ElbowLeft);
                DrawJointToJoint(dc, readings, sensors, JointType.ElbowLeft, JointType.WristLeft);
                DrawJointToJoint(dc, readings, sensors, JointType.WristLeft, JointType.HandLeft);

                DrawJointToJoint(dc, readings, sensors, JointType.ShoulderRight, JointType.ElbowRight);
                DrawJointToJoint(dc, readings, sensors, JointType.ElbowRight, JointType.WristRight);
                DrawJointToJoint(dc, readings, sensors, JointType.WristRight, JointType.HandRight);
                
                DrawJointToJoint(dc, readings, sensors, JointType.HipLeft, JointType.KneeLeft);
                DrawJointToJoint(dc, readings, sensors, JointType.KneeLeft, JointType.AnkleLeft);
                DrawJointToJoint(dc, readings, sensors, JointType.AnkleLeft, JointType.FootLeft);

                DrawJointToJoint(dc, readings, sensors, JointType.HipRight, JointType.KneeRight);
                DrawJointToJoint(dc, readings, sensors, JointType.KneeRight, JointType.AnkleRight);
                DrawJointToJoint(dc, readings, sensors, JointType.AnkleRight, JointType.FootRight);

            }
        }

        private void DrawJointToJoint(DrawingContext dc,List<SensorReadingPackage> readings, List<SensorDetails> sensors, JointType j1, JointType j2)
        {
            dc.DrawLine(
                trackedBonePen, 
                XlatePoint(GetPoint(readings, sensors, j1)), 
                XlatePoint(GetPoint(readings, sensors, j2)));
        }

        private Point XlatePoint(Point point)
        {
            return new Point(point.X-320,point.Y-240);
        }

        private Point GetPoint(List<SensorReadingPackage> readings, List<SensorDetails> sensors, JointType jointType)
        {
            SensorDetails sd = sensors.First(p => p.SensorName == jointType.ToString());

            SensorReadingPackage temp = readings.First(x => x.Id == sd.Id);

            return new Point(temp.Position.X,temp.Position.Y);
        }
    }
}
