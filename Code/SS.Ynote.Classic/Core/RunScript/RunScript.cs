using System.Collections.Generic;
using System.IO;
using System.Xml;
using SS.Ynote.Classic.Core.Settings;

namespace SS.Ynote.Classic.Core.RunScript
{
    public sealed class RunScript
    {
        /// <summary>
        ///     Script Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Script Process
        /// </summary>
        public string Process { get; private set; }

        /// <summary>
        ///     Script arguments
        /// </summary>
        public string Arguments { get; private set; }

        /// <summary>
        ///     Active Command Line Directory
        /// </summary>
        public string CmdDir { get; private set; }

        public static IEnumerable<string> GetConfigurations()
        {
            return Directory.GetFiles(GlobalSettings.SettingsDir, "*.ynoterun", SearchOption.AllDirectories);
        }

        public static RunScript ToRunConfig(string file)
        {
            using (var reader = XmlReader.Create(file))
                while (reader.Read())
                    if (reader.IsStartElement())
                        if (reader.Name == "Config")
                        {
                            var config = new RunScript
                            {
                                Name = reader["Name"],
                                Arguments = reader["Args"],
                                CmdDir = reader["CmdDir"],
                                Process = reader["Process"]
                            };
                            return config;
                        }
            return null;
        }

        public string GetPath()
        {
            return string.Format(@"{0}\RunScripts\{1}.run", GlobalSettings.SettingsDir, Name);
        }

        internal void ProcessConfiguration(string filename)
        {
            Arguments = Arguments.Replace("$source", filename).Replace("$source_dir", Path.GetDirectoryName(filename));
        }

        internal void EditConfig(string name, string proc, string args, string dir)
        {
            var str =
                string.Format(
                    "<?xml version=\"1.0\"?>\r\n\t<YnoteRun>\r\n\t\t<Config Name=\"{3}\" Process=\"{0}\" Args=\"{1}\" Directory=\"{2}\"/>\r\n\t</YnoteRun>",
                    proc, args, dir, name);
            Name = name;
            Arguments = args;
            CmdDir = dir;
            Process = proc;
            File.WriteAllText(string.Format("{0}\\RunScripts\\{1}.run", GlobalSettings.SettingsDir, name), str);
        }

        internal string ToBatch()
        {
            if (!string.IsNullOrEmpty(CmdDir))
                return string.Format("@echo off\r\necho {0} Run Script\r\ncd {1}\r\n{2} {3}", Name, CmdDir, Process,
                    Arguments);
            return string.Format("@echo off\r\necho {0} Run Script\r\n{1} {2}", Name, Process, Arguments);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}