using System;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

using i3statusbar.ClickEvents;

namespace i3statusbar.Blocks
{
    public class Updates : Block
    {
        private Process _updateProcess = new Process();
        private readonly string[] _updateCommands = {
                "/usr/bin/checkupdates", "/usr/bin/checkupdates-aur"
            };

        public int updateCount = 0;

        public Updates() : base(0x000000) 
        {
            Name = nameof(Updates);
            Separator = true;
            SeparatorWidth = 9;
            Active = false;
            
            _updateProcess.StartInfo.UseShellExecute = false;
            _updateProcess.StartInfo.RedirectStandardOutput = true;
            _updateProcess.StartInfo.RedirectStandardError = true;

            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(TimeSpan.FromDays(1));
                    await CheckUpdates();
                }
            });
        }
        
        private async Task CheckUpdates()
        {
            int updates = 0;

            foreach (string path in _updateCommands)
            {

                _updateProcess.StartInfo.FileName = path;
                for (int i = 0; i < 3; i++)
                {
                    if (!_updateProcess.Start()) 
                    {
                        return;
                    }
                    string output = await _updateProcess.StandardOutput.ReadToEndAsync();
                    _updateProcess.StandardError.ReadToEnd();
                    await _updateProcess.WaitForExitAsync();

                    if (_updateProcess.ExitCode == 0)
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
            Active = updateCount > 0;
            FullText = $"\uf381 {updateCount}";
        }

        public override void ProcessClickEvent(object sender, ClickEventArgs args)
        {
            HelperFunctions.LaunchApplication("/usr/bin/xterm", "-e \"yay; echo 'Press any key to continue'; read -sk\"");
            Task.Run(CheckUpdates);
        }
    }
}