using System;
using System.Collections.Generic;
using System.Text;

namespace Termometro.Application.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class TempHumViewModel
    {
        /// <summary>
        /// Data e hora da medição
        /// </summary>
        public DateTime DateTimeNow { get; set; } = DateTime.Now;

        /// <summary>
        /// Data e hora da medição em formato UTC
        /// </summary>
        public DateTime DateTimeNowUtc { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Temperatura em graus Celsius.
        /// </summary>
        public double Temperature { get; set; }

        /// <summary>
        /// Temperatura em graus Fahrenheit.
        /// </summary>
        public double TemperatureFahrenheit { get; set; }

        /// <summary>
        /// Porcentagem de umidade.
        /// </summary>
        public double HumidityPercentage { get; set; }
    }
}
