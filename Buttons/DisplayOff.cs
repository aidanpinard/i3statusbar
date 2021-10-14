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

        }
    }
}