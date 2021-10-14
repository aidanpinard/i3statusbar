using i3statusbar.ClickEvents;
using i3statusbar.Types;

using Newtonsoft.Json;

namespace i3statusbar.Blocks.SubBlocks
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class SubBlock
    {
        [JsonProperty("name")]
        public string Name { get; protected set; }

        private string _full_text = "";
        [JsonProperty("full_text")]
        public string FullText { 
            get
            {
                return _full_text;
            }
            protected set
            {
                _full_text = $" {value}";
            }
        }

        [JsonProperty("instance")]
        public string Instance { get; protected set; }

        [JsonProperty("short_text")]
        public string ShortText { get; protected set; }

        [JsonProperty("min_width")]
        public int MinWidth { get; protected set; }

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

        [JsonProperty("separator")]
        public bool Separator { get; protected set; }

        [JsonProperty("separator_block_width")]
        public int SeparatorWidth { get; protected set; }

        public SubBlock(Block parent)
        {
            Name = parent.Name;
            TextColour = parent.TextColour;
            Background = parent.Background;
            BorderColour = parent.BorderColour;
            BorderTop = parent.BorderTop;
            BorderBottom = parent.BorderBottom;
            BorderLeft = parent.BorderLeft;
            BorderRight = parent.BorderRight;
            Separator = parent.Separator;
            SeparatorWidth = parent.SeparatorWidth;
        }

        public abstract void Update();

        public abstract void ProcessClickEvent(object sender, ClickEventArgs args);

        public void Serialize(JsonWriter writer, JsonSerializer serializer)
        {
            serializer.Serialize(writer, this);
        }
    }
}