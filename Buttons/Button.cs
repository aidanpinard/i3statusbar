using i3statusbar.ClickEvents;

using Newtonsoft.Json;

namespace i3statusbar.Buttons
{
    
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class Button : BarSection
    {
        protected char _icon;

        [JsonProperty("full_text")]
        public override string FullText {
            get
            {
                return $" {_icon} ";
            }
        }
        
        [JsonProperty("name")]
        public override string Name { get; protected set; }
        
        public override bool Active { get; protected set; }

        public Button()
        {
            Active = true;
        }

        public abstract void ProcessClickEvent(object sender, ClickEventArgs args);

        public void Serialize(JsonWriter writer, JsonSerializer serializer)
        {
            serializer.Serialize(writer, this);
        }
    }
}