using OpenHardwareMonitor.Hardware;
using System;
using System.Linq;

namespace ChromaTemp
{
    class TemperatureRetriever
    {
        public static bool TryGetCpuTemperature(out int temp)
        {
            Computer computer = new Computer {CPUEnabled = true};
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

