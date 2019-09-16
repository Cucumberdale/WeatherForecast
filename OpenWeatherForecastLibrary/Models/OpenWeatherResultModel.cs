using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;

namespace OpenWeatherForecastLibrary.Models
{
    public class OpenWeatherResultModel
    {
        [Description("City data.")]
        public OpenWeatherCityModel City { get; set; }
        [Description("Forecast data.")]
        public List<OpenWeatherForecastModel> List { get; set; }
    }
}