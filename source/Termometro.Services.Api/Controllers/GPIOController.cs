using Microsoft.AspNetCore.Mvc;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Abstractions;

namespace Termometro.Services.Api.Controllers
{
    /// <summary>
    /// CPIO Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GPIOController : ControllerBase
    {
        /// <summary>
        /// Fornece acesso ao Raspberry PI GPIO e inverte o seu valor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Alterado o </response>
        /// <response code="400"></response>
        // GET api/values/5
        [HttpGet("{id}")]
        public bool Get(uint id)
        {
            var pin = Pi.Gpio[(int)id];
            pin.PinMode = GpioPinDriveMode.Output;
            var isOn = pin.Read();

            pin.Write(!isOn);

            return !isOn;
        }

    }
}