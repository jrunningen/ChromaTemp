using Corale.Colore.Core;
using System;
using System.Threading;

namespace ChromaTemp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                if (TemperatureRetriever.TryGetCpuTemperature(out int temp))
                {
                    var color = TemperatureToColorConverter.ConvertTemperature(temp);
                    //Chroma.Instance.Keyboard.SetAll(color);
                    Console.WriteLine($"Temp: {temp} | Color: {color.R}|{color.G}|{color.B}");
                }
                Thread.Sleep(3000);
            }
        }
    }
}
