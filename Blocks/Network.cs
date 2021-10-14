using System;
using System.Linq;
using System.Diagnostics;
using System.Net.NetworkInformation;

using i3statusbar.ClickEvents;

namespace i3statusbar.Blocks
{
    public class Network : Block
    {
        private enum BitSpeedUnits
        {
            bps, Kbps, Mbps, Gbps, Tbps
        }
        private enum ByteSpeedUnits
        {
            Bs, KiBs, MiBs, GiBs, TiBs
        }

        private NetworkInterface[] _interfaces = 
            NetworkInterface.GetAllNetworkInterfaces()
            .Where(iface => 
                iface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
            .ToArray();
        private Stopwatch _transferTime;
        
        private long _lastRx = 0;
        private long _lastTx = 0;
        private bool usebps = true;

        private static string ConvertToBps(double rate)
        {
            rate *= 8;
            BitSpeedUnits unit = BitSpeedUnits.bps;
            while (rate > 1000)
            {
                rate /= 1000.0;
                unit++;
            }
            return $"{rate:F1}{unit}";
        }

        private static string ConvertToBs(double rate)
        {
            ByteSpeedUnits unit = ByteSpeedUnits.Bs;
            while (rate > 1024)
            {
                rate /= 1024.0;
                unit++;
            }
            return $"{rate:F1}{unit.ToString().Replace("s", "/s")}";
        }

        public Network() : base(0x008000)
        {
            Name = nameof(Network);
            _lastRx = 
                _interfaces
                .Select(iface => iface.GetIPStatistics().BytesReceived)
                .Sum();
            _lastTx = 
                _interfaces
                .Select(iface => iface.GetIPStatistics().BytesSent)
                .Sum();
            _transferTime = Stopwatch.StartNew();
        }

        public override void Update() 
        {
            long rx = 
                _interfaces
                .Select(iface => iface.GetIPStatistics().BytesReceived)
                .Sum();
            long tx = 
                _interfaces
                .Select(iface => iface.GetIPStatistics().BytesSent)
                .Sum();
            double elapsed = _transferTime.Elapsed.TotalMilliseconds;
            _transferTime.Restart();
            
            char icon;
            if (_interfaces.Where(iface => iface.OperationalStatus == OperationalStatus.Up).FirstOrDefault().Name == "enp2s0") 
            {
                icon = '\uf796';
            }
            else if (_interfaces.Where(iface => iface.OperationalStatus == OperationalStatus.Up).FirstOrDefault().Name == "wlan0")
            {
                icon = '\uf1eb';
            }
            else if (_interfaces.Any(iface => iface.OperationalStatus == OperationalStatus.Up))
            {
                icon = '\uf6ff';
            }
            else
            {
                FullText = "\uf05e No Network Connection";
                ShortText = "\f00d";
                return;
            }

            double rx_rate = (rx - _lastRx) * 1000.0 / elapsed;
            double tx_rate = (tx - _lastTx) * 1000.0 / elapsed;

            Func<double, string> converter = usebps ? ConvertToBps : ConvertToBs;

            FullText = $"{icon} {converter(rx_rate)} \uf063 {converter(tx_rate)} \uf062";
            ShortText = $"{icon}";

            _lastRx = rx;
            _lastTx = tx;
        }

        public override void ProcessClickEvent(object sender, ClickEventArgs args)
        {
            if (args.Button == 3)
            {
                usebps = !usebps;
            }
            else
            {
                HelperFunctions.LaunchApplication("/usr/bin/xterm", "-e bandwhich");
            }
        }
    }
}