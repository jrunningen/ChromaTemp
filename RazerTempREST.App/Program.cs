using Corale.Colore.Core;
using System;
using System.Threading;

namespace RazerTempREST.App
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                if (TemperatureRetriever.TryGetCPUTemperature(out int temp))
                {
                    var color = TemperatureToColorConverter.ConvertTemperature(temp);
                    Chroma.Instance.Keyboard.SetAll(color);
                    Console.WriteLine($"Temp: {temp} | Color: {color.R}|{color.G}|{color.B}");
                }
                Thread.Sleep(3000);
            }
        }
    }
}
