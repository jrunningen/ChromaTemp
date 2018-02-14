using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazerTempREST.App
{
    class TemperatureRetriever
    {
        public static bool TryGetCPUTemperature(out int temp)
        {
            Computer computer = new Computer();
            computer.CPUEnabled = true;
            computer.Open();

            var tempSensor = computer.Hardware
                .SelectMany(h => h.Sensors)
                .FirstOrDefault(s => s.SensorType == SensorType.Temperature);

            if (tempSensor?.Value == null)
            {
                Console.WriteLine("Couldn't get a temperature.");
                temp = -1;
                return false;
            }

            temp =  (int)tempSensor.Value;
            return true;
        }
    }
}

