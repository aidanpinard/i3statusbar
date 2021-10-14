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
            bool use_inbuilt_separator;
            Types.Colour nextColour;

            
            Console.WriteLine(@"{ ""version"": 1, ""click_events"":true }");
            writer.WriteStartArray();
            while (true)
            {

                writer.WriteStartArray();
                
                use_inbuilt_separator = false;

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
                    BarSection section = activeSections[i];
                    section.Serialize(writer, serializer);
                    
                    if (section is Block block) 
                    {
                        if (!use_inbuilt_separator)
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
                await Task.Delay(1000);
            }
        }
    }
}