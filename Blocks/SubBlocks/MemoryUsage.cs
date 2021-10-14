using System.Linq;

using i3statusbar.ClickEvents;

namespace i3statusbar.Blocks.SubBlocks
{
    public class MemoryUsage : SubBlock
    {
        public MemoryUsage(Block parent) : base(parent)
        {
            Instance = nameof(MemoryUsage);                
        }

        public override void Update()
        {
            double percent = 
            HelperFunctions.RunCommand("/usr/bin/free", "-b")
                .Split('\n')
                .Skip(1)
                .FirstOrDefault()
                .Split(' ')
                .Where(str => !string.IsNullOrWhiteSpace(str))
                .Where((_, pos) => pos == 1 || pos == 6)
                .Select(str => (double)long.Parse(str))
                .Aggregate((x, y) => ((x-y)/x)*100);

            FullText = $"\uf538 {percent:F1}%";
        }

        public override void ProcessClickEvent(object sender, ClickEventArgs args)
        {
            
        }
    }
}