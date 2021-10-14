using System.Collections.Generic;
using System.IO;
using System.Linq;

using Newtonsoft.Json;

namespace i3statusbar.ClickEvents
{
    public class ClickEventHandler
    {
        private readonly List<BarSection> _sections;
        public ClickEventHandler(List<BarSection> sections)
        {
            _sections = sections;
        }

        public void Listen(TextReader input)
        {
            // i3bar sends as infinite array. first line is '['
            // and following lines are ',' then the objects(except first click)
            string line = input.ReadLine();
            ClickEventArgs args;
            while (true)
            {
                line = input.ReadLine().TrimStart(','); // remove leading , if exists
                args = JsonConvert.DeserializeObject<ClickEventArgs>(line);
                _sections
                    .Where(section => section.Name == args.Name)
                    .FirstOrDefault()?.ProcessClickEvent(this, args);
            }
        }
    }
}