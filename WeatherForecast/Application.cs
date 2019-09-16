using System;
using System.Collections.Generic;
using System.Linq;
using OpenWeatherForecastLibrary.Models;
using OpenWeatherForecastLibrary.Utilities;

namespace WeatherForecast
{
    public class Application : IApplication
    {
        private readonly IOpenWeatherDataAccess _openWeatherDataAccess;

        public Application(IOpenWeatherDataAccess openWeatherDataAccess)
        {
            _openWeatherDataAccess = openWeatherDataAccess;
        }

        public void Run(string[] args)
        {
            switch (args.Length)
            {
                case 1:
                    ProcessForecastData(args[0], null, null, false);
                    break;

                case 2:
                    IdentifyTwoArguments(args);
                    break;

                case 3:
                    IdentifyThreeArguments(args);
                    break;

                case 4:
                    IdentifyFourArguments(args);
                    break;

                default:
                    Console.WriteLine("Please enter arguments <CityName> <CountryCode>(Optional) <Units>(\"metric\" or \"imperial\", Optional) \"advanced\"(Optional)");
                    break;

            }
        }

        private void IdentifyTwoArguments(IReadOnlyList<string> args)
        {
            switch (args[1])
            {
                case "metric":
                case "imperial":
                    ProcessForecastData(args[0], null, args[1], false);
                    break;

                case "advanced":
                    ProcessForecastData(args[0], null, null, true);
                    break;

                default:
                    ProcessForecastData(args[0], args[1], null, false);
                    break;
            }
        }

        private void IdentifyThreeArguments(IReadOnlyList<string> args)
        {
            switch (args[2])
            {
                case "metric":
                case "imperial":
                    ProcessForecastData(args[0], args[1], args[2], false);
                    break;

                case "advanced":
                    if (args[1] == "metric" || args[1] == "imperial")
                    {
                        ProcessForecastData(args[0], null, args[1], true);
                    }
                    else
                    {
                        ProcessForecastData(args[0], args[1], null, true);
                    }
                    break;

                default:
                    Console.WriteLine("Please enter arguments <CityName> <CountryCode>(Optional) <Units>(\"metric\" or \"imperial\", Optional) \"advanced\"(Optional)");
                    break;
            }
        }

        private void IdentifyFourArguments(IReadOnlyList<string> args)
        {
            if ((args[2] == "metric" || args[2] == "imperial") && args[3] == "advanced")
            {
                ProcessForecastData(args[0], args[1], args[2], true);
            }

            else Console.WriteLine("Please enter arguments <CityName> <CountryCode>(Optional) <Units>(\"metric\" or \"imperial\", Optional) \"advanced\"(Optional)");
        }

        private void ProcessForecastData(string cityName, string countryCode, string units, bool advanced)
        {
            var cities = _openWeatherDataAccess.FindCityModelByNameAndCountrycode(cityName, countryCode);

            switch (cities.Count)
            {
                case 0:
                    Console.Write("City could not be found in the database.\nTry entering city name in its native language (i.e. Warszawa)." +
                                  "\nIf city name consists of multiple word enclose them in quotation marks (i.e. \"Lone Pine\")");
                    break;

                case 1:
                {
                    var forecast = _openWeatherDataAccess.LoadForecastData(cities.First().Id, units);
                    if (advanced) DisplayAdvancedForecastData(forecast, units);
                    else DisplaySimpleForecastData(forecast, units);
                    break;
                }

                default:
                {
                    Console.WriteLine("Multiple cities found.");
                    var i = 1;

                    foreach (var city in cities)
                    {
                            Console.WriteLine($"{ i }. { city.Name }, { city.Country }, geo location: { city.Coord.Lat } Latitude { city.Coord.Lon } Longitude");
                            ++i;
                    }

                    Console.Write("Choose city from list: ");
                    var choice = Console.ReadLine();

                    while (!int.TryParse(choice, out var potentialChoiceNumber) || potentialChoiceNumber-1 >= cities.Count)
                    {
                        Console.Write("Choose city from list: ");
                        choice = Console.ReadLine();
                    }

                    int.TryParse(choice, out var choiceNumber);
                    var forecast = _openWeatherDataAccess.LoadForecastData(cities[choiceNumber-1].Id, units);
                    if (advanced) DisplayAdvancedForecastData(forecast, units);
                    else DisplaySimpleForecastData(forecast, units);
                    break;
                }
            }
        }

        private static void DisplayAdvancedForecastData(OpenWeatherResultModel forecast, string units)
        {
            if (forecast == null) throw new ArgumentException("Forecast can't be null.", nameof(forecast));

            string temperatureUnit;
            string windSpeedUnit;
            string dateFormat;
            switch (units)
            {
                case null:
                    temperatureUnit = "K";
                    windSpeedUnit = "meters/second";
                    dateFormat = "dd/MM/yyyy";
                    break;
                case "metric":
                    temperatureUnit = "C";
                    windSpeedUnit = "meters/second";
                    dateFormat = "dd/MM/yyyy";
                    break;
                default:
                    temperatureUnit = "F";
                    windSpeedUnit = "miles/hour";
                    dateFormat = "MM/dd/yyyy";
                    break;
            }

            Console.WriteLine($"\nForecast for { forecast.City.Name }, { forecast.City.Country }.\n");

            foreach (var day in forecast.List)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Date: { new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(day.Dt + forecast.City.Timezone).ToString(dateFormat)}");
                Console.ResetColor();
                Console.WriteLine($"Sunrise: { new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(day.Sunrise + forecast.City.Timezone).TimeOfDay }" +
                                  $"\tSunset: { new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(day.Sunset + forecast.City.Timezone).TimeOfDay }");
                Console.WriteLine($"Temperature: Max. { day.Temp.Max } { temperatureUnit }  Min. { day.Temp.Min } { temperatureUnit }  Morn. { day.Temp.Morn } { temperatureUnit }  " +
                                  $"Day { day.Temp.Day } { temperatureUnit }  Eve. { day.Temp.Eve } { temperatureUnit }  Night {day.Temp.Night} { temperatureUnit }");
                Console.WriteLine($"Sky: { day.Weather.First().Description }\tCloudiness: { day.Clouds }%");
                Console.WriteLine($"Pressure: { day.Pressure } hPa");
                Console.WriteLine($"Humidity: { day.Humidity }%");
                Console.WriteLine($"Wind speed: { day.Speed } { windSpeedUnit }\tWind direction: { day.Deg } degrees (meteorological)\n");
            }
        }

        private static void DisplaySimpleForecastData(OpenWeatherResultModel forecast, string units)
        {
            if (forecast == null) throw new ArgumentException("Forecast can't be null.", nameof(forecast));

            string temperatureUnit;
            string dateFormat;
            switch (units)
            {
                case null:
                    temperatureUnit = "K";
                    dateFormat = "dd/MM/yyyy";
                    break;
                case "metric":
                    temperatureUnit = "C";
                    dateFormat = "dd/MM/yyyy";
                    break;
                default:
                    temperatureUnit = "F";
                    dateFormat = "MM/dd/yyyy";
                    break;
            }

            Console.WriteLine($"\nForecast for { forecast.City.Name }, { forecast.City.Country }.\n");

            foreach (var day in forecast.List)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Date: { new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(day.Dt + forecast.City.Timezone).ToString(dateFormat)}");
                Console.ResetColor();
                Console.WriteLine($"Sunrise: { new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(day.Sunrise + forecast.City.Timezone).TimeOfDay }" +
                                  $"\tSunset: { new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(day.Sunset + forecast.City.Timezone).TimeOfDay }");
                Console.WriteLine($"Temperature: Max. { day.Temp.Max } { temperatureUnit }  Min. { day.Temp.Min } { temperatureUnit }  Morn. { day.Temp.Morn } { temperatureUnit }  " +
                                  $"Day { day.Temp.Day } { temperatureUnit }  Eve. { day.Temp.Eve } { temperatureUnit }  Night {day.Temp.Night} { temperatureUnit }");
                Console.WriteLine($"Sky: { day.Weather.First().Description }\tCloudiness: { day.Clouds }%\n");
            }
        }

        
    }
}