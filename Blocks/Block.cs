using System.Collections.Generic;

using i3statusbar.Blocks.SubBlocks;
using i3statusbar.ClickEvents;
using i3statusbar.Types;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace i3statusbar.Blocks
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class Block : BarSection
    {
        public override bool Active { get; protected set; }
        private string _full_text = "";
        [JsonProperty("full_text")]
        public override string FullText { 
            get
            {
                return _full_text;
            }
            protected set
            {
                _full_text = $" {value}";
            }
        }

        [JsonProperty("short_text")]
        public string ShortText { get; protected set; }

        [JsonProperty("color")]
        public Colour TextColour { get; protected set; }

        [JsonProperty("background")]
        public Colour Background { get; protected set; }

        [JsonProperty("border")]
        public Colour BorderColour { get; protected set; }

        [JsonProperty("border_top")]
        public int BorderTop { get; protected set; }

        [JsonProperty("border_bottom")]
        public int BorderBottom { get; protected set; }

        [JsonProperty("border_left")]
        public int BorderLeft { get; protected set; }

        [JsonProperty("border_right")]
        public int BorderRight { get; protected set; }

        [JsonProperty("min_width")]
        public int? MinWidth { get; protected set; }

        [JsonProperty("name")]
        public override string Name { get; protected set; }

        [JsonProperty("instance")]
        public string Instance { get; protected set; }

        [JsonProperty("separator")]
        public bool Separator { get; protected set; }

        [JsonProperty("separator_block_width")]
        public int SeparatorWidth { get; protected set; }

        public Dictionary<string, SubBlock> SubBlocks = new Dictionary<string, SubBlock>();

        [JsonObject(MemberSerialization.OptIn)]
        public class BlockSeparator
        {
            [JsonProperty("full_text")]
            public const string FullText = " \ue0b2";

            [JsonProperty("color")]
            private Colour TextColour = new Colour(0x000000);

            [JsonProperty("background")]
            private Colour Background = new Colour(0x000000);

            [JsonProperty("border")]
            public readonly Colour BorderColour = new Colour(0x000000);

            [JsonProperty("border_top")]
            public const int BorderTop = 2;

            [JsonProperty("border_bottom")]
            public const int BorderBottom = 2;

            [JsonProperty("border_left")]
            public const int BorderLeft = 0;

            [JsonProperty("border_right")]
            public const int BorderRight = 0;

            [JsonProperty("separator")]
            public const bool Separator = false;

            [JsonProperty("separator_block_width")]
            public const int SeparatorWidth = 0;

            public int SetColours(int first, int second)
            {
                Background.Code = first;
                TextColour.Code = second;
                return second;
            }

            public void Serialize(JsonWriter writer, JsonSerializer serializer)
            {
                serializer.Serialize(writer, this);
            }
        }

        public Block(int background) {
            Active = true;
            Background = new Colour(background);
            BorderColour = new Colour(0x000000);
            Separator = false;
            SeparatorWidth = 0;
            BorderTop = 2;
            BorderBottom = 2;
            BorderLeft = 0;
            BorderRight = 0;
        }

        public abstract void Update();

        public abstract void ProcessClickEvent(object sender, ClickEventArgs args);

        public void Serialize(JsonWriter writer, JsonSerializer serializer)
        {
            if (SubBlocks.Count > 0)
            {
                foreach (SubBlock subBlock in SubBlocks.Values)
                {
                    subBlock.Serialize(writer, serializer);
                }
            }
            else
            {
                serializer.Serialize(writer, this);
            }
        }
    }
}