using Autofac;
using OpenWeatherForecastLibrary.Helpers;

namespace WeatherForecast
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ApiHelper.InitializeClient();
            var container = ContainerConfig.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IApplication>();
                app.Run(args);
            }
        }
    }
}
