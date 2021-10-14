using i3statusbar.ClickEvents;

namespace i3statusbar.Buttons
{
    public class Logout : Button
    {
        public Logout()
        {
            Name = nameof(Logout);
            _icon = '\uf011';
        }

        public override void ProcessClickEvent(object sender, ClickEventArgs args)
        {

        }
    }
}