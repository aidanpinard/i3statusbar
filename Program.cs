using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using i3statusbar.Blocks;
using i3statusbar.Buttons;

using Newtonsoft.Json;

namespace i3statusbar
{
    class Program
    {
        private static readonly List<Block> _blocks = 
            new List<Block>
            {
                new Network(),
                new VPN(),
                new Metrics(),
                new Battery(),
                new Volume(),
                new Time(),
                new Updates()
            };
        private static readonly List<Button> _buttons = 
            new List<Button>
            {

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

            Console.WriteLine(@"{ ""version"": 1, ""click_events"":true }");

            writer.WriteStartArray();
            Block.BlockSeparator separator = new Block.BlockSeparator();
            while (true)
            {
                writer.WriteStartArray();
                int previous_colour = 0x000000;
                bool use_inbuilt_separator = false;
                
                // all blocks **should** be independent of each other
                Parallel.ForEach(_blocks, (block) => block.Update());

                foreach(Block block in _blocks.Where(block => block.Active))
                {                    
                    if (!use_inbuilt_separator)
                    {
                        previous_colour = 
                            separator.SetColours(previous_colour, block.Background.Code);
                        separator.Serialize(writer, serializer);
                    }

                    use_inbuilt_separator = block.Separator;
                    block.Serialize(writer, serializer);
                }

                // add it to the end just in case the last one isn't using inbuilt
                if (!use_inbuilt_separator)
                {
                    separator.SetColours(previous_colour, 0x000000);
                    separator.Serialize(writer, serializer);
                }

                foreach (Button button in _buttons.Where(button => button.Active))
                {
                    button.Serialize(writer, serializer);
                }

                writer.WriteEndArray();
                Console.WriteLine();
                Console.Out.Flush();
                System.Threading.Thread.Sleep(1000);
            }
        }

        private static void Main(string[] args)
        {
            DisplayAndUpdate();
        }
    }
}
