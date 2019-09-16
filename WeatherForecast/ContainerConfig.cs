using System.Linq;
using System.Reflection;
using Autofac;

namespace WeatherForecast
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Application>().As<IApplication>();

            builder.RegisterAssemblyTypes(Assembly.Load(nameof(OpenWeatherForecastLibrary)))
                .Where(t => t.Namespace != null && t.Namespace.Contains("Utilities"))
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name));

            return builder.Build();
        }
    }
}