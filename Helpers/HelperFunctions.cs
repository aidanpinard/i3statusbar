using System;
using System.Diagnostics;
using System.IO;

namespace i3statusbar
{
    public class HelperFunctions
    {
        public static void LaunchApplication(string executable, string args = null)
        {
            using Process command = new Process();
            command.StartInfo.FileName = executable;
            command.StartInfo.Arguments = args;
            
            command.StartInfo.UseShellExecute = true;
            command.Start();
        }

        public static string RunCommand(string executable, string args = null)
        {
            using Process command = new Process();

            command.StartInfo.FileName = executable;
            command.StartInfo.Arguments = args;
            command.StartInfo.UseShellExecute = false;
            command.StartInfo.RedirectStandardOutput = true;
            command.StartInfo.RedirectStandardError = true;

            if (!command.Start()) 
            {
                Environment.Exit(2);
            }

            string output = command.StandardOutput.ReadToEnd();
            command.StandardError.ReadToEnd();
            command.WaitForExit();
            
            return output;
        }
    
        public static string GetContent(string path)
        {
            return File.ReadAllText(path);
        }
    }
}