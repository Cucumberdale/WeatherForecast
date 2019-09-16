using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualBasic.CompilerServices;
using OpenWeatherForecastLibrary.Models;

namespace OpenWeatherForecastLibrary.Utilities
{
    public interface IOpenWeatherDataAccess
    {
        OpenWeatherResultModel LoadForecastData(int cityId, string units);
        List<OpenWeatherCityModel> FindCityModelByNameAndCountrycode(string name, string countrycode);
    }
}