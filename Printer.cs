using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using i3statusbar.Blocks;

using Newtonsoft.Json;

namespace i3statusbar
{
    public class Printer
    {
        private readonly List<BarSection> _sections;
        public Printer(List<BarSection> sections)
        {
            _sections = sections;
        }

        public async Task PrintOutput(TextWriter output, List<Types.Colour> colours = null)
        {
            JsonWriter writer = new JsonTextWriter(output);
            JsonSerializer serializer = 
                new JsonSerializer {
                    Formatting = Formatting.None,
                    NullValueHandling = NullValueHandling.Ignore
                };
            
            Block.BlockSeparator separator = new Block.BlockSeparator();

            List<BarSection> activeSections;
            Types.Colour nextColour;

            Console.WriteLine(@"{ ""version"": 1, ""click_events"":true }");
            writer.WriteStartArray();
            while (true)
            {
                writer.WriteStartArray();

                // all blocks **should** be independent of each other
                _sections
                    .OfType<Block>()
                    .AsParallel()
                    .ForAll((block) => block.Update());
                
                activeSections = _sections.Where(section => section.Active).ToList();

                if (activeSections.First() is Block firstBlock) 
                {
                    separator.SetColours(Types.Colour.Black, firstBlock.Background);
                    separator.Serialize(writer, serializer);   
                } 

                for(int i = 0; i < activeSections.Count; i++)
                {
                    activeSections[i].Serialize(writer, serializer);
                    
                    if (activeSections[i] is Block block) 
                    {
                        if (!block.Separator)
                        {
                            if (activeSections[(i + 1) % (activeSections.Count - 1)] is Block nextBlock)
                            {
                                nextColour = nextBlock.Background;
                            }
                            else
                            {
                                nextColour = Types.Colour.Black;
                            }
                            separator.SetColours(block.Background, nextColour);
                            separator.Serialize(writer, serializer);
                        }
                    }
                }

                writer.WriteEndArray();
                Console.WriteLine();
                Console.Out.Flush();
                await Task.Delay(1000);
            }
        }
    }
}