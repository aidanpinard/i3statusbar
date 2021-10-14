using System.Linq;

using i3statusbar.ClickEvents;

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
                FullText = $"\uf205 {connections[0].Split(':').FirstOrDefault()} \uf233";
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

        }
    }
}