using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using OpenWeatherForecastLibrary.Helpers;
using OpenWeatherForecastLibrary.Models;

namespace OpenWeatherForecastLibrary.Utilities
{
    public class OpenWeatherDataAccess : IOpenWeatherDataAccess
    {
        public OpenWeatherResultModel LoadForecastData(int cityId, string units)
        {
            var url = $"https://openweathermap.org/data/2.5/forecast/daily/?appid=b6907d289e10d714a6e88b30761fae22&id={ cityId }&units={ units }";

            using (var response = ApiHelper.ApiClient.GetAsync(url).Result)
            {
                if (!response.IsSuccessStatusCode) throw new Exception(response.ReasonPhrase);

                var openWeatherSimpleForecast = response.Content.ReadAsAsync<OpenWeatherResultModel>().Result;
                return openWeatherSimpleForecast;
            }
        }

        public List<OpenWeatherCityModel> FindCityModelByNameAndCountrycode(string name, string countrycode)
        {
            using (var reader = new StreamReader("Resources/city.list.json"))
            {
                var json = reader.ReadToEnd();
                var cities = JsonConvert.DeserializeObject<List<OpenWeatherCityModel>>(json);

                if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("City name is missing.", nameof(name));

                if (string.IsNullOrWhiteSpace(countrycode))
                {
                    return cities
                        .Where(city => city.Name == name)
                        .ToList();
                }

                return cities
                    .Where(city => city.Name == name && city.Country == countrycode)
                    .ToList();
            }
        }
    }
}