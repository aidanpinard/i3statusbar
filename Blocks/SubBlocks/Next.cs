using i3statusbar.ClickEvents;
namespace i3statusbar.Blocks.SubBlocks
{
    public class Next : SubBlock
    {
        public Next(Block parent) : base(parent)
        {
            Instance = nameof(Next);
        }
        
        public override void Update()
        {
            FullText = "\uf04e";
        }

        public override void ProcessClickEvent(object sender, ClickEventArgs args)
        {
            HelperFunctions.LaunchApplication("/usr/bin/playerctl", "next");
        }
    }
}