
public class WeatherReport : IWeatherReport
    {
        public static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        public void ReportWeather()
        {
            Console.WriteLine("1.ReportWeather");
        }
        public void ReportWeather2()
        {
           Console.WriteLine("2.ReportWeather");
        }
    }