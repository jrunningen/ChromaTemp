using Corale.Colore.Core;

namespace ChromaTemp
{
    class TemperatureToColorConverter
    {
        private static readonly Color Red = new Color(255, 0, 0);
        private static readonly Color Cyan = new Color(00, 255, 255);

        public static Color ConvertTemperature(int temp)
        {
            // 30 degrees: cyan
            // 40  - 
            // 50  - blue
            // 60  - green
            // 70  - yellow
            // 80  - orange
            // 90+ - red

            if (temp < 30)
            {
                return Cyan;
            }
            if (temp < 50)
            {
                // Interpolate cyan and blue
                int scale = (temp - 30) / 2; // FIXME: Maybe scale by float.
                return new Color(0, ScaleDown(scale), 255);
            }
            if (temp < 60)
            {
                int scale = temp - 50;
                // Interpolate blue and green
                return new Color(0, ScaleUp(scale), ScaleDown(scale));
            }
            if (temp < 70)
            {
                int scale = temp - 60;
                // Interpolate green and yellow
                return new Color(ScaleUp(scale), 255, 0);
            }
            if (temp < 90)
            {
                int scale = (temp - 70) / 2;
                // Interpolate yellow, orange, and red
                return new Color(255, ScaleDown(scale), 0);
            }

            return Red;
        }

        private static int ScaleUp(int factor)
        {
            return (int)(25.5 * factor);
        }

        private static int ScaleDown(int factor)
        {
            return (int)(255 - (25.5 * factor));
        }
    }
}
