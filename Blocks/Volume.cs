using i3statusbar.ClickEvents;

namespace i3statusbar.Blocks
{
    public class Volume : Block
    {
        public Volume() : base(0x673ab7) 
        {
            Name = nameof(Volume);
        }

        public override void Update() 
        {
            string volume = HelperFunctions.RunCommand("/usr/bin/pamixer", "--get-volume-human");
            volume = volume.Substring(0, volume.Length-1);
            char icon = '\uf026';
            if (volume == "muted")
            {
                icon = '\uf6a9';
                volume = "0%";
            }
            else if (int.TryParse(volume, out int vol) && vol > 50)
            {
                icon = '\uf028';
            }
            else if (vol > 0)
            {
                icon = '\uf027';
            }

            FullText = $"{icon} {volume}";
            ShortText = FullText;
        }

        public override void ProcessClickEvent(object sender, ClickEventArgs args)
        {
            HelperFunctions.LaunchApplication("/usr/bin/xterm", "alsamixer");
        }
    }
}