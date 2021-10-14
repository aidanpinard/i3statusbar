using System.Collections.Generic;

using i3statusbar.Blocks.SubBlocks;

namespace i3statusbar.Blocks
{
    /* // TODO IMplement this properly rename to audio
    public class Player : Block
    {
        private Dictionary<string, SubBlock> _subBlocks;

        public Player() : base(0xd14081, 0x000000)
        {
            _subBlocks = new Dictionary<string, SubBlock> 
                {
                    { nameof(Previous), new Previous{Name = Name} },
                    { nameof(NowPlaying), new NowPlaying{Name = Name} },
                    { nameof(Next), new Next{Name = Name} }
                };
        }

        public override void Update() 
        {
            foreach(SubBlock playerButtons in _subBlocks.Values)
            {
                playerButtons.Update();
            }
        }

        public override void ProcessClickEvent(object sender, ClickEventArgs args)
        {
            if (_subBlocks.TryGetValue(args.Instance, out SubBlock _clickedSubBlock))
            {
                _clickedSubBlock.ProcessClickEvent(sender, args);
            }
        }
    }
    */
}