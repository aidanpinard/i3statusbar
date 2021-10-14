using System.Linq;

using i3statusbar.ClickEvents;

using Notify;

namespace i3statusbar.Blocks
{
    public class VPN : Block
    {
        public VPN() : base(0x424242) 
        {
            Name = nameof(VPN);
        }

        public override void Update() 
        {
            string[] connections =
                HelperFunctions.RunCommand("/usr/bin/nmcli", "-t -f TYPE,NAME connection show --active")
                .Split('\n')
                .Where(line => line.StartsWith("vpn"))
                .ToArray();
            
            if (connections.Length > 0)
            {
                FullText = $"\uf205 {connections[0].Split(':').Skip(1).FirstOrDefault()} \uf233";
                ShortText = "\uf205 VPN \uf233";
                Background.Code = 0xE53935;
            }
            else
            {
                FullText = "\uf204 VPN \uf233";
                Background.Code = 0x424242;
            }
        }

        public override void ProcessClickEvent(object sender, ClickEventArgs args)
        {
            string[] connections =
                HelperFunctions.RunCommand("/usr/bin/nmcli", "-t -f TYPE,NAME connection show --active")
                .Split('\n')
                .Where(line => line.StartsWith("vpn"))
                .ToArray();
            
            Notification notification;
            if (connections.Length > 0)
            {
                notification = new Notification("VPN Disonnection", 
                    $"Connection to {connections[0].Split(':').Skip(1).FirstOrDefault()} has been ended.",
                    1000, "network-server");
                HelperFunctions.RunCommand("/usr/bin/nmcli", 
                    $"con down {connections[0].Split(':').Skip(1).FirstOrDefault()}");
            }
            else
            {
                notification = new Notification("VPN Connection", 
                    $"Connection to US_miami-UDP has been created.",
                    1000, "network-server");
                HelperFunctions.RunCommand("/usr/bin/nmcli", "con up US_miami-UDP");
            }
            notification.Show();
        }
    }
}