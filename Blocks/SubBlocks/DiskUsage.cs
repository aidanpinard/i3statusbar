using System.Linq;
using System.IO;

using i3statusbar.ClickEvents;

namespace i3statusbar.Blocks.SubBlocks
{
    public class DiskUsage : SubBlock
    {
        public DiskUsage(Block parent) : base(parent)
        {
            Instance = nameof(DiskUsage);
        }
         
        public override void Update()
        {
            System.Collections.Generic.IEnumerable<DriveInfo> drives = 
                DriveInfo.GetDrives()
                .Where(drive => 
                    drive.DriveType == DriveType.Fixed 
                    || drive.DriveType == DriveType.Network 
                    || drive.Name == "/boot");

            double freeSpace = drives.Sum(drive => drive.AvailableFreeSpace);
            double totalSpace = drives.Sum(drive => drive.TotalSize);
            FullText = $"\uf0a0 {(totalSpace - freeSpace)*100.0/totalSpace:F1}%";
        }

        public override void ProcessClickEvent(object sender, ClickEventArgs args)
        {
            
        }
    }
}