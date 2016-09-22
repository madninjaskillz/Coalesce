using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using CoalesceInputPlugin;
using CoalesceOutputPlugin;
using CoalesceTypes;

namespace coalesce
{
    public static class CoreSolids
    {
        public class PluginDetails
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public Type Type { get; set; }
            public string Author { get; set; }
        }

        private static string execPath => System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        public static List<PluginDetails> InputPlugins { get; set; } = new List<PluginDetails>();
        public static List<PluginDetails> OutputPlugins { get; set; } = new List<PluginDetails>();

        public static void UpdateInputPlugs()
        {
            InputPlugins = LoadPlugins<ICoalesceInputPlugin>("Inputs");
            OutputPlugins = LoadPlugins<ICoalesceOutputPlugin>("Outputs");


            Debug.WriteLine(InputPlugins.Count + " input plugins loaded");
        }


        public static List<PluginDetails> LoadPlugins<T>(string path)
        {
            var files = Directory.GetFiles($"Plugins\\{path}\\", "*.dll").ToList();
            var plugins = new List<PluginDetails>();
            foreach (string file in files)
            {
                Assembly assembly = Assembly.LoadFrom(execPath + "\\" + file);

                var types = assembly.GetExportedTypes();

                foreach (Type type in types)
                {
                    Debug.WriteLine(type);
                    if (typeof(T).IsAssignableFrom(type))
                    {
                        ICoalescePlugin instance = (ICoalescePlugin)Activator.CreateInstance(type);
                        CoalescePlugInDetails details = instance.GetDetails();

                        plugins.Add(new PluginDetails()
                        {
                            Id = details.Id,
                            Name = details.ShortName,
                            Type = type,
                            Author = details.Author
                        });
                    }
                }
            }

            return plugins;
        }


    }

  
}







 