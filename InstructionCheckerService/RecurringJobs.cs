using Hangfire;

// public static class RecurringJobs
// {
//       public static void AddRecurringJobs()
//       {
//             // RecurringJob.AddOrUpdate<IRecurringJobService>(
//             //     "RecurringJobService.SendEmail",
//             //     x => x.SendEmail(),
//             //     Cron.Minutely);
//       }
//       public static void HandleInstructions()
//       {
//             WeatherReport weather = new();

//             RecurringJob.RemoveIfExists(nameof(weather.ReportWeather));
//             RecurringJob.RemoveIfExists(nameof(weather.ReportWeather2));
//             RecurringJob.RemoveIfExists(nameof(weather.ReportWeather2) + "copy");

//             // RecurringJob.AddOrUpdate<IWeatherReport>(nameof(weather.ReportWeather), x =>
//             //       x.ReportWeather(), Cron.Daily(16, 15), TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time")); //16:15
//             // RecurringJob.AddOrUpdate<IWeatherReport>(nameof(weather.ReportWeather2), x =>
//             //       x.ReportWeather2(), Cron.MinuteInterval(1), TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time"));//2 dakkada bir
//             // RecurringJob.AddOrUpdate<IWeatherReport>(nameof(weather.ReportWeather2) + "copy", x =>
//             //       x.ReportWeather2(), Cron.Daily(17, 02), TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time"));//17:15
//       }
// }