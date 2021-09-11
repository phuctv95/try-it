using WebApi.Hal;

namespace TryHal.Representations
{
    public static class LinkTemplates
    {
        private const string Host = "https://localhost:5001";

        public static class WeatherForecasts
        {
            public static WebApi.Hal.Link Today => new Link("today", $"{Host}/WeatherForecast/today");
            public static WebApi.Hal.Link Tomorrow => new Link("tomorrow", $"{Host}/WeatherForecast/tomorrow");
        }
    }
}
