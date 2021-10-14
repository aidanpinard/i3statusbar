using System.Collections.Generic;

using i3statusbar.Blocks.SubBlocks;

namespace i3statusbar.Blocks
{
    public class Metrics : Block
    {
        public Metrics() : base(0x3949AB)
        {
            Name = nameof(Metrics);
            SubBlocks = new Dictionary<string, SubBlock> 
                {
                    { nameof(CpuUtilization), new CpuUtilization(this) },
                    { nameof(CpuTemp), new CpuTemp(this) },
                    { nameof(CpuFanSpeed), new CpuFanSpeed(this) },
                    { nameof(MemoryUsage), new MemoryUsage(this) },
                    { nameof(DiskUsage), new DiskUsage(this) }
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