using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace OpenWeatherForecastLibrary.Models
{
    public class OpenWeatherForecastModel
    {
        [Description("Time of forecasted data, seconds.")]
        public int Dt { get; set; }
        [Description("Time of sunrise, seconds.")]
        public int Sunrise { get; set; }
        [Description("Time of sunset, seconds.")]
        public int Sunset { get; set; }
        [Description("Temperature, Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit.")]
        public OpenWeatherTemperatureModel Temp { get; set; }
        [Description("Atmospheric pressure on the sea level, hPa.")]
        public float Pressure { get; set; }
        [Description(" Humidity, %.")]
        public float Humidity { get; set; }
        [Description("Sky cloudiness.")]
        public List<OpenWeatherSkyModel> Weather { get; set; }
        [Description("Wind speed, Default: meter/sec, Metric: meter/sec, Imperial: miles/hour.")]
        public float Speed { get; set; }
        [Description("Wind direction, degrees (meteorological).")]
        public int Deg { get; set; }
        [Description("Cloudiness, %")]
        public int Clouds { get; set; }
    }
}