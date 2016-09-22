using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Windows.Threading;
using CoalesceInputPlugin;
using Timer = System.Timers.Timer;

namespace coalesce.UI.MainWindow
{
    public class MainWindowViewModel : BaseViewModel
    {


        private SensorDetails selectedSensor;
        public SensorDetails SelectedSensor
        {
            get { return selectedSensor; }
            set { Set(ref selectedSensor, value); }
        }

        private InputList selectInputPlugin = new InputList();
        public InputList SelectedInputPlugin
        {
            get { return selectInputPlugin; }
            set { Set(ref selectInputPlugin, value); }
        }

        private List<InputList> inputPlugins = new List<InputList>();
        public List<InputList> InputPlugins
        {
            get { return inputPlugins; }
            set { Set(ref inputPlugins, value); }
        }

        private ObservableCollection<InputList> addedInputPlugins = new ObservableCollection<InputList>();
        public ObservableCollection<InputList> AddedInputPlugins
        {
            get { return addedInputPlugins; }
            set { Set(ref addedInputPlugins, value); }
        }

        private InputList selectedAddedInputPlugin;
        public InputList SelectedAddedInputPlugin
        {
            get { return selectedAddedInputPlugin; }
            set
            {
                Set(ref selectedAddedInputPlugin, value);
                UpdateCurrentInput(value);
            }
        }


        private List<SensorDetails> currentSensors = new List<SensorDetails>();
        public List<SensorDetails> CurrentSensors
        {
            get { return currentSensors; }
            set { Set(ref currentSensors, value); }
        }

        private void UpdateCurrentInput(InputList plugin)
        {
            List<SensorDetails> t = plugin.Plugin.GetSensors();
            foreach (SensorDetails sensorDetailse in t)
            {
                sensorDetailse.Parent = plugin.InstanceId;
            }

            CurrentSensors = t;
        }

        private string title = "test";

        public string Title
        {
            get { return title; }
            set { Set(ref title, value); }
        }



        private string currentSelectedJointPlugin = "";

        public string CurrentSelectedJointPlugin
        {
            get { return currentSelectedJointPlugin; }
            set { Set(ref currentSelectedJointPlugin, value); }
        }


        private string currentSelectedJointSensor = "";

        public string CurrentSelectedJointSensor
        {
            get { return currentSelectedJointSensor; }
            set { Set(ref currentSelectedJointSensor, value); }
        }


        public double CurrentSensorX { get; set; }
        public double CurrentSensorY { get; set; }
        public double CurrentSensorZ { get; set; }

        
        public void Init()
        {
            InputPlugins = new List<InputList>();
            List<InputList> tempList = new List<InputList>();
            foreach (var item in CoreSolids.InputPlugins)
            {

                tempList.Add(new InputList
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }

            InputPlugins = tempList;
            // Create a timer with a two second interval.
            Timer aTimer = new System.Timers.Timer(10);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private bool timerCodeRunning = false;
        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            if (timerCodeRunning)
            {
                return;
            }

            timerCodeRunning = true;
            try
            {
                foreach (var plugin in addedInputPlugins)
                {
                    List<SensorReadingPackage> readings = plugin.Plugin.GetReadings();
                    allReadings[plugin.InstanceId] = readings;
                }

                if (Assignments.ContainsKey(selectedJoint))
                {
                    Assignment ass = Assignments[selectedJoint];


                    if (allReadings.ContainsKey(ass.PluginInstance))
                    {
                        List<SensorReadingPackage> reading = allReadings[ass.PluginInstance];

                        SensorReadingPackage sens = reading.FirstOrDefault(y => y.Id == ass.SensorId);
                        if (sens != null)
                        {
                            //TODO - check for positional
                            {
                                CurrentSensorX = sens.Position.X;
                                CurrentSensorY = sens.Position.Y;
                                CurrentSensorZ = sens.Position.Z;

                                Debug.WriteLine($"{CurrentSensorX},{CurrentSensorY},{CurrentSensorZ}");
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            timerCodeRunning = false;
        }

  
        private Dictionary<Guid, List<SensorReadingPackage>> allReadings = new Dictionary<Guid, List<SensorReadingPackage>>();

        public void AddInput()
        {
            if (SelectedInputPlugin != null)
            {
                var plugin = CoreSolids.InputPlugins.First(t => t.Id == SelectedInputPlugin.Id);
                Debug.WriteLine(plugin);
                ICoalesceInputPlugin newInstance = (ICoalesceInputPlugin)Activator.CreateInstance(plugin.Type);
                newInstance.Initialise();
                InputList newItem = new InputList
                {
                    Id = selectInputPlugin.Id,
                    Name = selectInputPlugin.Name,
                    InstanceId = Guid.NewGuid(),
                    Plugin = newInstance
                };

                AddedInputPlugins.Add(newItem);
            }
        }

        public void AddInput(InputList pluginToAdd)
        {
                var plugin = CoreSolids.InputPlugins.First(t => t.Id == pluginToAdd.Id);
                Debug.WriteLine(plugin);
                ICoalesceInputPlugin newInstance = (ICoalesceInputPlugin)Activator.CreateInstance(plugin.Type);
                newInstance.Initialise();
                InputList newItem = new InputList
                {
                    Id = pluginToAdd.Id,
                    Name = pluginToAdd.Name,
                    InstanceId = Guid.NewGuid(),
                    Plugin = newInstance
                };

                AddedInputPlugins.Add(newItem);
        }


        public Dictionary<DestinationJoints, Assignment> Assignments = new Dictionary<DestinationJoints, Assignment>();

        public class Assignment
        {
            public string SensorKey { get; set; }
            public string SensorName { get; set; }
            public string PluginName { get; set; }
            public List<IModifier> Modifiers { get; set; }
            public Guid PluginId { get; set; }
            public Guid PluginInstance { get; set; }
            public int SensorId { get; set; }
            public PlugInDetails PluginDetails { get; set; }
        }

        public void Assign(DestinationJoints destJoint)
        {
            selectedJoint = destJoint;

            if (Assignments.ContainsKey(destJoint))
            {
                var dja = Assignments[destJoint];

                CurrentSelectedJointPlugin = dja.PluginName;
                CurrentSelectedJointSensor = dja.SensorName;

                DispatcherTimer temp=new DispatcherTimer
                {
                    IsEnabled = true,
                    Interval = TimeSpan.FromMilliseconds(100),
                };

                temp.Tick += (sender, args) =>
                {
                    CurrentSelectedJointPlugin = dja.PluginName;
                    CurrentSelectedJointSensor = dja.SensorName;
                    temp.Stop();
                };

                temp.Start();
            }
        }

        private DestinationJoints selectedJoint;

        public void BindPosition()
        {
            if (!Assignments.ContainsKey(selectedJoint))
            {
                Assignments.Add(selectedJoint, new Assignment());
            }

            Assignment ass = Assignments[selectedJoint];
            //ass.Plugin = SelectedSensor;

            ICoalesceInputPlugin plugin = AddedInputPlugins.First(x => x.InstanceId == SelectedSensor.Parent).Plugin;
            ass.PluginDetails = plugin.GetDetails();
            ass.PluginName = plugin.GetDetails().ShortName;
            ass.SensorName = SelectedSensor.SensorName;
            ass.SensorKey = SelectedSensor.Parent + "-" + SelectedSensor.Id;
            ass.SensorId = SelectedSensor.Id;
            ass.PluginId = ass.PluginDetails.Id;
            ass.PluginInstance = SelectedSensor.Parent;


            CurrentSelectedJointPlugin = ass.PluginName;
            CurrentSelectedJointSensor = ass.SensorName;
        }
    }
}
