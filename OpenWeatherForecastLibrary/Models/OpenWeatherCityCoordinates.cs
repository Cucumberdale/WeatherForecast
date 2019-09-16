using System.ComponentModel;

namespace OpenWeatherForecastLibrary.Models
{
    public class OpenWeatherCityCoordinates
    {
        [Description("City geo location latitude.")]
        public float Lat { get; set; }
        [Description("City geo location longitute.")]
        public float Lon { get; set; }
    }
}