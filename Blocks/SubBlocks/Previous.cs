using System;
using System.Collections.Generic;
using System.Linq;
using i3statusbar.ClickEvents;

namespace i3statusbar.Blocks.SubBlocks
{
    public class Previous : SubBlock
    {
        public Previous(Block parent) : base(parent)
        {
            Instance = nameof(Previous);
        }

        public override void Update()
        {
            FullText = "\uf04a";
        }

        public override void ProcessClickEvent(object sender, ClickEventArgs args)
        {
            HelperFunctions.LaunchApplication("/usr/bin/playerctl", "previous");
        }
    }
}