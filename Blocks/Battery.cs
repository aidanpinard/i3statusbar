using System.Linq;

namespace i3statusbar.Blocks
{
    public class Battery : Block
    {
        private const string _battery_status_path = "/sys/class/power_supply/BAT1/uevent";
        private const string _charge_threshold_path = "/sys/class/power_supply/BAT1/charge_control_end_threshold";

        private void ScaleColor(float percent)
        {
            int.TryParse(HelperFunctions.GetContent(_charge_threshold_path), out int maxCharge);
            float scaled_percent = percent / maxCharge;
            if (scaled_percent > 1)
            {
                scaled_percent = 1;
            }

            if (scaled_percent <= 0.5)
            {
                Background.Red = (int)(scaled_percent * 2 * 0xff);
            }
            else 
            {
                Background.Green = 0xff - (int)((scaled_percent - 50) * 0xff);
            }
        }

        public Battery() : base(0xD69E2E)
        {
            Name = nameof(Battery);
            TextColour = new Types.Colour(0x000000);
        }

        public override void Update() 
        {
            string batteryData = 
                HelperFunctions.GetContent(_battery_status_path);
            string capacity = 
                batteryData
                .Split('\n')
                .Where(str => str.StartsWith("POWER_SUPPLY_CAPACITY"))
                .FirstOrDefault()
                .Split("=")
                .Skip(1)
                .FirstOrDefault();
            if (string.IsNullOrWhiteSpace(capacity) 
                || !int.TryParse(capacity, out int percentage))
            {
                FullText = $"\uf0e7 \uf128%";
                return;
            }

            string status = 
                batteryData
                .Split('\n')
                .Where(str => str.StartsWith("POWER_SUPPLY_STATUS"))
                .FirstOrDefault()
                .Split("=")
                .Skip(1)
                .FirstOrDefault();

            //f1e6 = charging
            if (status == "Charging") 
            {
                FullText = $"\uf1e6 {percentage}%";
            }
            else if (percentage > 90)
            {
                FullText = $"\uf240 {percentage}%";
            }
            else if (percentage > 75)
            {
                FullText = $"\uf241 {percentage}%";
            }
            else if (percentage > 50)
            {
                FullText = $"\uf242 {percentage}%";
            }
            else if (percentage > 25)
            {
                FullText = $"\uf243 {percentage}%";
            }
            else
            {
                FullText = $"\uf244 {percentage}%";
            }
        }

        public override void ProcessClickEvent(object sender, ClickEventArgs args)
        {

        }
    }
}