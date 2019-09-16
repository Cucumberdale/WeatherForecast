using System.Collections;
using System.Collections.Generic;

namespace WeatherForecast.Tests.TestData
{
    public class OpenWeatherDataAccessTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "London", "GB", new List<int>
            {
                2643743
            } };
            yield return new object[] { "London", null, new List<int>
            {
                2643743,5056033,6058560,4119617,4298960,4517009,5367815
            } };
            yield return new object[] { "London", "", new List<int>
            {
                2643743,5056033,6058560,4119617,4298960,4517009,5367815
            } };
            yield return new object[] { "London", "  ", new List<int>
            {
                2643743,5056033,6058560,4119617,4298960,4517009,5367815
            } };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}