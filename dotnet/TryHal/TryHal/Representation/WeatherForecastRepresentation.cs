using WebApi.Hal;

namespace TryHal.Representations
{
    public class WeatherForecastRepresentation : Representation
    {
        public override string Rel
        {
            get => LinkTemplates.WeatherForecasts.Today.Rel;
            set {}
        }
        public override string Href
        {
            get => LinkTemplates.WeatherForecasts.Today.CreateLink().Href;
            set {}
        }
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        public string? Summary { get; set; }

        protected override void CreateHypermedia()
        {
            // not call to here, don't know why.
            // Links.Add(LinkTemplates.WeatherForecasts.Tomorrow.CreateLink());
        }
    }
}
