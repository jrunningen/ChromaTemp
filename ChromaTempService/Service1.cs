using System;
using System.ServiceProcess;
using System.Threading;
using ChromaTemp;
using Corale.Colore.Core;

namespace ChromaTempService
{
    public partial class Service1 : ServiceBase
    {
        // FIXME: This kinda works, but I'm not doing any proper synchronization.
        // There's no mutex guarding shouldRun, and other synchronization primitives
        // would almost certainly be a better fit here. I'm just not at all familiar
        // with System.Threading.
        private Thread th;
        Boolean shouldRun;

        public Service1()
        {
            InitializeComponent();
            th = new Thread(SetColorLoop);
            th.IsBackground = true;
            shouldRun = false;
        }

        private void SetColorLoop()
        {
            // FIXME: Sommetimes my keyboard turns black for three seconds while this
            // loop is running.
            // FIXME: Add proper logging beyond Console.Writeline. Windows Event Logs,
            // I guess?
            while (shouldRun)
            {
                if (TemperatureRetriever.TryGetCpuTemperature(out int temp))
                {
                    var color = TemperatureToColorConverter.ConvertTemperature(temp);
                    Chroma.Instance.Keyboard.SetAll(color);
                    Console.WriteLine($"Temp: {temp} | Color: {color.R}|{color.G}|{color.B}");
                }
                Thread.Sleep(3000);
            }
        }

        protected override void OnStart(string[] args)
        {
            shouldRun = true;
            th.Start();
        }

        protected override void OnStop()
        {
            shouldRun = false;
            th.Join();
        }
    }
}
