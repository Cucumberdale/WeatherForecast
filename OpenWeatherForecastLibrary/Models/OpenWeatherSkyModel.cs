using System.ComponentModel;

namespace OpenWeatherForecastLibrary.Models
{
    public class OpenWeatherSkyModel
    {
        [Description("Group of weather parameters (Rain, Snow, Extreme etc.)")]
        public string Main { get; set; }
        [Description("Weather condition within the group")]
        public string Description { get; set; }
    }
}