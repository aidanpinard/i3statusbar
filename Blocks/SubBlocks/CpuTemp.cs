using System;
using System.Linq;

using i3statusbar.ClickEvents;

namespace i3statusbar.Blocks.SubBlocks
{
    public class CpuTemp : SubBlock
    {   
        private const string _sensors_path = "/usr/bin/sensors";
        public CpuTemp(Block parent) : base(parent)
        {
            Instance = nameof(CpuTemp);
        }

        public override void Update()
        {
            string sensorTemp = 
                HelperFunctions.RunCommand(_sensors_path, "k10temp-pci-00c3 -j")
                .Split(':')
                .LastOrDefault()
                .Split('}')
                .FirstOrDefault()
                .Trim();

            if (!float.TryParse(sensorTemp, out float temp))
            {
                FullText = "\uf769 \uf128°C";
                return;
            } 
            temp = (float)Math.Round(temp, 1);
            if (temp >= 75.0)
            {
                FullText = $"\uf769 {temp:F1}°C";
            }
            else
            {
                FullText = $"\uf76b {temp:F1}°C";
            }
        }

        public override void ProcessClickEvent(object sender, ClickEventArgs args)
        {
            
        }
    }
}