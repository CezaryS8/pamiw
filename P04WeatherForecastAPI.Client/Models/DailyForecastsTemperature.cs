using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.Models
{
    internal class DailyForecastsTemperature
    {
        public Metric Minimum { get; set; }
        public Metric Maximum { get; set; }
    }
}
