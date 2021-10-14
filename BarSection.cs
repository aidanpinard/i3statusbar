namespace i3statusbar
{
    public class BarSection
    {
        public virtual string FullText { get; protected set; }
        public virtual bool Active { get; protected set; }
        public virtual string Name { get; protected set; }
    }
}