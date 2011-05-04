using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace FileWatcher
{
    class Program
    {
        private static FileSystemWatcher watcher;
        private static Regex fileRegex;
        private static string command;
        private static string arguments;

        static void Main(string[] args)
        {
            watcher = new FileSystemWatcher(args[0]);
            fileRegex = new Regex(args[1]);
            command = args[2];
            arguments = args[3];

            while(true)
            {
                var change = watcher.WaitForChanged(WatcherChangeTypes.Changed | WatcherChangeTypes.Created | WatcherChangeTypes.Renamed);
                if (fileRegex.IsMatch(change.Name))
                    new Action<string>(Execute).BeginInvoke(change.Name, null, null);
            }
        }

        static void Execute(string file)
        {
            var cmd = ReplaceVars(command, file);
            var args = ReplaceVars(arguments, file);

            Console.WriteLine("{0} Executing {1} {2}", DateTime.Now.ToString("hh:MM:ss"), cmd, args);

            var p = new Process {StartInfo = new ProcessStartInfo(cmd, args)};
            p.StartInfo.WorkingDirectory = watcher.Path;
            p.Start();
        }

        static string ReplaceVars(string str, string file)
        {
            return str.Replace(":wefile", Path.GetFileNameWithoutExtension(file)).Replace(":file", file).Replace(":dir", watcher.Path);
        }
    }
}
