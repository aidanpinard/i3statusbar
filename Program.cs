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

        private static void DisplayAndUpdate() {
            JsonWriter writer = new JsonTextWriter(Console.Out);
            JsonSerializer serializer = 
                new JsonSerializer {
                    Formatting = Formatting.None,
                    NullValueHandling = NullValueHandling.Ignore
                };

            List<Types.Colour> colours = new List<Types.Colour>();
            _sections.Aggregate((BarSection)null, (first, last) => {
                if (last is Block block)
                {
                    colours.Add(block.Background);
                }
                return first;
            });
            
            Console.WriteLine(@"{ ""version"": 1, ""click_events"":true }");

            writer.WriteStartArray();
            Block.BlockSeparator separator = new Block.BlockSeparator();
            while (true)
            {
                writer.WriteStartArray();
                int colourIdx = 0;
                bool use_inbuilt_separator = false;
                
                separator.SetColours(new Types.Colour(0x000000), colours[colourIdx++]);
                separator.Serialize(writer, serializer);
                // all blocks **should** be independent of each other
                _sections
                    .OfType<Block>()
                    .AsParallel()
                    .ForAll((block) => block.Update());

                foreach(BarSection section in _sections.Where(block => block.Active))
                {
                    section.Serialize(writer, serializer);
                    
                    if (section is Block block) 
                    {
                        if (!use_inbuilt_separator)
                        {
                            separator.SetColours(block.Background, colours[colourIdx++]);
                            separator.Serialize(writer, serializer);
                        }

                        use_inbuilt_separator = block.Separator;
                    }
                    else
                    {
                        use_inbuilt_separator = true;
                    }
                }

                writer.WriteEndArray();
                Console.WriteLine();
                Console.Out.Flush();
                System.Threading.Thread.Sleep(1000);
            }
        }

        private static void Main(string[] args)
        {
            if (_sections.Count == 0)
            {
                Environment.ExitCode = 1;
                return;
            }
            ClickEventHandler ceh = new ClickEventHandler(_sections);
            DisplayAndUpdate();
        }
    }
}
