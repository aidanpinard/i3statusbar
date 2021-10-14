using System;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;


namespace i3statusbar.Blocks
{
    public class Updates : Block
    {
        private Stopwatch _waitHour = new Stopwatch();

        public int updateCount = 0;

        public Updates() : base(0x000000) 
        {
            Name = nameof(Updates);
            Separator = true;
            SeparatorWidth = 9;
            Active = false;
        }
        
        private async Task CheckUpdates()
        {
            int updates = 0;
            using Process command = new Process();

            string[] update_commands = {
                "/usr/bin/checkupdates", "/usr/bin/checkupdates-aur"
            };

            foreach (string path in update_commands)
            {
                command.StartInfo.FileName = path;
                command.StartInfo.UseShellExecute = false;
                command.StartInfo.RedirectStandardOutput = true;
                command.StartInfo.RedirectStandardError = true;

                for (int i = 0; i < 3; i++)
                {
                    if (!command.Start()) 
                    {
                        return;
                    }
                    string output = await command.StandardOutput.ReadToEndAsync();
                    command.StandardError.ReadToEnd();
                    await command.WaitForExitAsync();

                    if (command.ExitCode == 0)
                    {
                        updates += output.Count(c => c == '\n');
                        break;
                    }
                }
            }
            Interlocked.Exchange(ref updateCount, updates);
        }

        public override void Update() 
        {
            if (_waitHour.Elapsed.TotalHours > 0 || !_waitHour.IsRunning)
            {
                Task.Run(CheckUpdates);
                _waitHour.Restart();
            }
        
            Active = updateCount > 0;

            FullText = $"\uf381 {updateCount}";
        }

        public override void ProcessClickEvent(object sender, ClickEventArgs args)
        {

        }
    }
}