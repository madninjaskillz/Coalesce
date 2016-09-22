using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CoalesceInputPlugin;

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

        private static string execPath=> System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        public static List<PluginDetails> InputPlugins { get; set; } = new List<PluginDetails>();

        public static void UpdateInputPlugs()
        {
            InputPlugins.Clear();
            List<string> files = Directory.GetFiles("Plugins\\Inputs\\", "*.dll").ToList();

            foreach (string file in files)
            {
                Assembly assembly = Assembly.LoadFrom(execPath+"\\"+file);

                var types = assembly.GetExportedTypes();

                foreach (Type type in types)
                {
                    Debug.WriteLine(type);
                    if (typeof(ICoalesceInputPlugin).IsAssignableFrom(type))
                    {
                        var instance = (ICoalesceInputPlugin) Activator.CreateInstance(type);
                        var details = instance.GetDetails();

                        InputPlugins.Add(new PluginDetails()
                        {
                            Id = details.Id,
                            Name = details.ShortName,
                            Type = type,
                            Author = details.Author
                        });
                    }
                }
            }

            Debug.WriteLine(InputPlugins.Count+" input plugins loaded");
        }
    }
}
