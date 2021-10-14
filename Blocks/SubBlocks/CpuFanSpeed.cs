using System.Linq;

using i3statusbar.ClickEvents;

namespace i3statusbar.Blocks.SubBlocks
{
    public class CpuFanSpeed : SubBlock
    {   
        private const string _sensors_path = "/usr/bin/sensors";
        public CpuFanSpeed(Block parent) : base(parent)
        {
            Instance = nameof(CpuFanSpeed);
            ShortText = "";
        }

        public override void Update()
        {
            string sensorTemp = 
                HelperFunctions.RunCommand(_sensors_path, "asus-isa-0000 -j")
                .Split(':')
                .LastOrDefault()
                .Split('.')
                .FirstOrDefault()
                .Trim();

            if (!float.TryParse(sensorTemp, out float temp))
            {
                FullText = "\uf863 \uf128 RPM";
            }
            else
            {
                FullText = $"\uf863 {temp:F0} RPM";
            }
        }

        public override void ProcessClickEvent(object sender, ClickEventArgs args)
        {
            HelperFunctions.LaunchApplication("/usr/bin/xterm", "-e watch -n 1 sensors");
        }
    }
}