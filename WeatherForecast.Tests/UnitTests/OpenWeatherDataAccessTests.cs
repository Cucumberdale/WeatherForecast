using System;
using System.Collections.Generic;
using System.Linq;
using OpenWeatherForecastLibrary.Utilities;
using WeatherForecast.Tests.TestData;
using Xunit;

namespace WeatherForecast.Tests.UnitTests
{
    public class OpenWeatherDataAccessTests
    {
        [Theory]
        [ClassData(typeof(OpenWeatherDataAccessTestData))]
        public void FindCityModelByNameAndCountrycode_ShouldWorkIfCityNameIsNotEmpty(string name, string countrycode, List<int> expectedIds)
        {
            var openWeatherDataAccess = new OpenWeatherDataAccess();

            var actual = openWeatherDataAccess.FindCityModelByNameAndCountrycode(name, countrycode);
            var actualIds = actual.Select(element => element.Id).ToList();

            Assert.Equal(expectedIds.Count, actual.Count);
            for (var i = 0; i < expectedIds.Count; ++i)
            {
                Assert.Equal(expectedIds[i], actualIds[i]);
            }
        }

        [Fact]
        public void FindCityModelByNameAndCountrycode_ShouldFailIfCityNameIsEmpty()
        {
            var openWeatherDataAccess = new OpenWeatherDataAccess();

            var exception = Assert.Throws<ArgumentException>(() => openWeatherDataAccess.FindCityModelByNameAndCountrycode("", "PL"));

            Assert.Equal("name", exception.ParamName);
        }
    }
}