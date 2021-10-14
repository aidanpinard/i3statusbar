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
            int volume = int.Parse(HelperFunctions.RunCommand("/usr/bin/pamixer", "--get-volume"));
            char icon = '\uf026';
            if (HelperFunctions.RunCommand("/usr/bin/pamixer", "--get-mute") == "true")
            {
                icon = '\uf6a9';
                volume = 0;
            }
            else if (volume > 50)
            {
                icon = '\uf028';
            }
            else if (volume > 0)
            {
                icon = '\uf027';
            }

            FullText = $"{icon} {volume}%";
            ShortText = FullText;
        }

        public override void ProcessClickEvent(object sender, ClickEventArgs args)
        {

        }
    }
}