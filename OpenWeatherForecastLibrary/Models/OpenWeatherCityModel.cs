using System.ComponentModel;

namespace OpenWeatherForecastLibrary.Models
{
    public class OpenWeatherCityModel
    {
        [Description("City id.")]
        public int Id { get; set; }
        [Description("City name.")]
        public string Name { get; set; }
        [Description("Country code.")]
        public string Country { get; set; }
        [Description("City geo location coordinates.")]
        public OpenWeatherCityCoordinates Coord { get; set; }
        [Description("Shift in seconds from UTC")]
        public int Timezone { get; set; }
    }
}