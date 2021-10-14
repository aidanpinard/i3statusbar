using i3statusbar.ClickEvents;

using Newtonsoft.Json;

namespace i3statusbar
{
    public abstract class BarSection
    {
        public virtual string FullText { get; protected set; }
        public virtual bool Active { get; protected set; }
        public virtual string Name { get; protected set; }

        public abstract void ProcessClickEvent(object sender, ClickEventArgs args);

        public abstract void Serialize(JsonWriter writer, JsonSerializer serializer);
    }
}