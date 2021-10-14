using System.Threading.Tasks;

using i3statusbar.ClickEvents;

namespace i3statusbar.Buttons
{
    public class DisplayOff : Button
    {
        public DisplayOff()
        {
            Name = nameof(DisplayOff);
            _icon = '\uf26c';
        }

        public override void ProcessClickEvent(object sender, ClickEventArgs args)
        {
            Task.Run(async () => {
                await Task.Delay(1000);
                HelperFunctions.LaunchApplication("/usr/bin/xset", "dpms force off");
            });
        }
    }
}