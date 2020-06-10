using Microsoft.AspNetCore.Mvc;
using System;
using Termometro.Application.ViewModels;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Abstractions;
using Unosquare.RaspberryIO.Peripherals;
using Unosquare.WiringPi;

namespace Termometro.Services.Api.Controllers
{
    /// <summary>
    /// Sensor DHT11 Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DHT11Controller : ControllerBase
    {
        /// <summary>
        /// Fornecer a temperatura e humidade 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public TempHumViewModel GetTemperatura()
        {
            Pi.Init<BootstrapWiringPi>();

            var sensor = DhtSensor.Create(DhtType.Dht11, Pi.Gpio[BcmPin.Gpio04]);
            var model = new TempHumViewModel();
            var valid = false;

            sensor.OnDataAvailable += (s, e) =>
            {
                if (!e.IsValid)
                    return;

                Console.WriteLine($"DHT11 Temperature: \n {e.Temperature:0.00}°C \n {e.TemperatureFahrenheit:0.00}°F  \n Humidity: {e.HumidityPercentage:P0}\n\n");
                model.HumidityPercentage = e.HumidityPercentage;
                model.Temperature = e.Temperature;
                model.TemperatureFahrenheit = e.TemperatureFahrenheit;
                model.DateTimeNow = DateTime.Now;
                model.DateTimeNowUtc = DateTime.UtcNow;

                valid = true;
            };

            sensor.Start();

            while (valid) { };

            return model;
        }
    }
}