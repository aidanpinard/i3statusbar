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
            if (args.Modifiers.Contains("Control"))
            {
                HelperFunctions.RunCommand("/usr/bin/i3-nagbar", "-t warning -m \"Shutdown?\" -b \"yes\" \"shutdown -h now\"");
            }
            else
            {
                HelperFunctions.RunCommand("/usr/bin/i3-nagbar", "-t warning -m \"Log out?\" -b \"yes\" \"i3-msg exit\"");
            }
        }
    }
}