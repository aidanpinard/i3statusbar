using System;

using i3statusbar.ClickEvents;

namespace i3statusbar.Blocks
{
    public class Time : Block
    {
        public Time() : base (0xe0e0e0) 
        {
            Name = nameof(Time);
            TextColour = new Types.Colour(0x000000);
        }

        public override void Update() 
        {
            DateTime currentTime = DateTime.Now;
            FullText = $"\uf783 {currentTime:ddd d, MMM} \uf017 {currentTime:h:mm tt}";
            ShortText = $"\uf017 {currentTime:h:mm tt}";
        }

        public override void ProcessClickEvent(object sender, ClickEventArgs args)
        {

        }
    }
}