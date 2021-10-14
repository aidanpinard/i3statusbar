using System;
using System.Diagnostics;
using System.Linq;

namespace i3statusbar.Blocks.SubBlocks
{
    public class CpuUtilization : SubBlock
    {
        private Stopwatch _measuredTime = Stopwatch.StartNew();
        private double _lastCpuTime = Process.GetProcesses().Sum(a => a.TotalProcessorTime.TotalMilliseconds);
        private double GetOverallCpuUsagePercentage()
        {
            double cpuTime = Process.GetProcesses().AsParallel().Sum((proc) => {
                try 
                {
                    return proc.TotalProcessorTime.TotalMilliseconds;
                }
                catch
                {
                    return 0.0;
                }
            });
            
            double usage = Math.Round((cpuTime - _lastCpuTime) / (Environment.ProcessorCount * _measuredTime.Elapsed.TotalMilliseconds) * 100, 1);
            _lastCpuTime = cpuTime;
            _measuredTime.Restart();
            return usage;
        }

        public CpuUtilization(Block parent) : base(parent)
        {
            Instance = nameof(CpuUtilization);
        }

        public override void Update()
        {
            FullText = $"\uf3fd {GetOverallCpuUsagePercentage(),4:F1}%";
        }

        public override void ProcessClickEvent(object sender, ClickEventArgs args)
        {
            
        }
    }
}