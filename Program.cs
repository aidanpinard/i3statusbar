using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using i3statusbar.Blocks;
using i3statusbar.Buttons;
using i3statusbar.ClickEvents;

using Newtonsoft.Json;

namespace i3statusbar
{
    class Program
    {
        private static readonly List<BarSection> _sections = 
            new List<BarSection>
            {
                new Network(),
                new VPN(),
                new Metrics(),
                new Battery(),
                new Volume(),
                new Time(),
                new Updates(),
                new DisplayOff(),
                new Logout()
            };

        private static void Main(string[] args)
        {
            if (_sections.Count == 0)
            {
                Environment.ExitCode = 1;
                return;
            }
            
            Printer printer = new Printer(_sections);
            ClickEventHandler clickEventHandler = new ClickEventHandler(_sections);
            printer.PrintOutput(Console.Out);
            clickEventHandler.Listen(Console.In);
        }
    }
}
