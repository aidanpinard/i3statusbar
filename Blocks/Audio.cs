using System.Collections.Generic;

using i3statusbar.Blocks.SubBlocks;
using i3statusbar.ClickEvents;

namespace i3statusbar.Blocks
{
    public class Audio : Block
    {
        public Audio() : base(0x000047)
        {
            Name = nameof(Metrics);
            SubBlocks = new Dictionary<string, SubBlock> 
                {
                    { nameof(Previous), new Previous(this) },
                    { nameof(NowPlaying), new NowPlaying(this) },
                    { nameof(Next), new Next(this) }
                };
        }

        public override void Update() 
        {
            foreach(SubBlock metric in SubBlocks.Values)
            {
                metric.Update();
            }
        }

        public override void ProcessClickEvent(object sender, ClickEventArgs args)
        {
            if (SubBlocks.TryGetValue(args.Instance, out SubBlock _clickedSubBlock))
            {
                _clickedSubBlock.ProcessClickEvent(sender, args);
            }
        }
    }
}