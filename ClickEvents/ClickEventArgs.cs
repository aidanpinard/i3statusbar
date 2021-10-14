using System.Collections.Generic;

using Newtonsoft.Json;

namespace i3statusbar.ClickEvents
{
    public class ClickEventArgs
    {
        [JsonProperty("name")]
        public string Name { get; private set; }
        
        [JsonProperty("instance")]
        public string Instance { get; private set; }
        
        [JsonProperty("button")]
        public int Button { get; private set; }
        
        [JsonProperty("modifiers")]
        public HashSet<string> Modifiers { get; private set; }
        
        [JsonProperty("x")]
        public int AbsoluteX { get; private set; }
        
        [JsonProperty("y")]
        public int AbsoluteY { get; private set; }
        
        [JsonProperty("relative_x")]
        public int RelativeX { get; private set; }
        
        [JsonProperty("relative_y")]
        public int RelativeY { get; private set; }
        
        [JsonProperty("output_x")]
        public int OutputX { get; private set; }
        
        [JsonProperty("output_y")]
        public int OutputY { get; private set; }
        
        [JsonProperty("width")]
        public int Width { get; private set; }
        
        [JsonProperty("height")]
        public int Height { get; private set; }
    }
}