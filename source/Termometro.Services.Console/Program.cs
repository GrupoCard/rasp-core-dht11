using System;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Abstractions;
using Unosquare.RaspberryIO.Peripherals;
using Unosquare.WiringPi;

namespace Termometro.Services.Console
{
    class Program
    {
        private const string ExitMessage = "Pressione a tecla Esc para continuar . . .";

        static void Main(string[] args)
        {
            Pi.Init<BootstrapWiringPi>();

            TestTempSensor();
        }

        /// <summary>
        /// Tests the temperature sensor.
        /// The DHT11 sensor, also available as KY-015 for usage on experimental boards, needs to be connected to Gpio04 (physical pin 7) with its
        /// single data line. The sensor has a conspicuous blue grid-shaped housing. 
        /// </summary>
        public static void TestTempSensor()
        {
            System.Console.Clear();

            using var sensor = DhtSensor.Create(DhtType.Dht11, Pi.Gpio[BcmPin.Gpio04]);
            var totalReadings = 0.0;
            var validReadings = 0.0;

            sensor.OnDataAvailable += (s, e) =>
            {
                totalReadings++;
                if (!e.IsValid)
                    return;

                System.Console.Clear();
                validReadings++;
                System.Console.WriteLine($"DHT11 Temperatura: \n {e.Temperature:0.00}°C \n {e.TemperatureFahrenheit:0.00}°F  \n Humidade: {e.HumidityPercentage:P0}\n\n");
                System.Console.WriteLine($"      Número de amostras de dados válidas recebidas: {validReadings} de {totalReadings}");
                System.Console.WriteLine();
                System.Console.WriteLine(ExitMessage);
            };

            sensor.Start();

            while (true)
            {
                var input = System.Console.ReadKey(true).Key;
                if (input != ConsoleKey.Escape) continue;

                break;
            }
        }
    }
}
