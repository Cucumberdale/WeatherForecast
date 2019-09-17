# WeatherForecast
Console application displaying 14-day weather forecast for chosen city.

## Usage
Compiled application can be found at https://mega.nz/#F!MjpikQJa!0eb3JdJHj0wJGMvpx9_piA.

To run the application you need to execute:

    dotnet .\WeatherForecast.dll <CityName> <CountryCode>* <metric/imperial>* <advanced>*
*Optional arguments

\<CountryCode> - specifies country code (there are 18 Londons in the USA, 5 of which are present in city.list.json).

\<metric/imperial> - sets the units to metric/imperial, default units are metric with temperature in Kelvin.

\<advanced> - displays additional forecast information (pressure, humidity, wind speed and direction).

Argument order is important (e.g. "dotnet .\WeatherForecast.dll Warszawa PL metric" will run but "dotnet .\WeatherForecast.dll Warszawa metric PL" will not)
