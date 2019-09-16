using System.ComponentModel;

namespace OpenWeatherForecastLibrary.Models
{
    public class OpenWeatherTemperatureModel
    {
        [Description("Day temperature. Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit.")]
        public float Day { get; set; }
        [Description("Min daily temperature. Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit.")]
        public float Min { get; set; }
        [Description("Max daily temperature. Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit.")]
        public float Max { get; set; }
        [Description("Night temperature. Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit.")]
        public float Night { get; set; }
        [Description("Evening temperature. Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit.")]
        public float Eve { get; set; }
        [Description("Morning temperature. Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit.")]
        public float Morn { get; set; }
    }
}