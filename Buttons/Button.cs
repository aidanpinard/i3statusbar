using i3statusbar.ClickEvents;

using Newtonsoft.Json;

namespace i3statusbar.Buttons
{
    
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class Button
    {
        protected char _icon;

        [JsonProperty("full_text")]
        public string FullText {
            get
            {
                return $" {_icon} ";
            }
        }
        
        [JsonProperty("name")]
        public string Name { get; protected set; }
        
        public bool Active { get; protected set; }

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