using Microsoft.AspNetCore.Mvc;
using prototype_csharp_alchemy_helper_api.Models;

namespace prototype_csharp_alchemy_helper_api.Controllers;

[ApiController]
public class WeatherForecasts
{
    private static string[] summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    [HttpGet()]
    [Route("/weatherforecast")]
    public ActionResult<WeatherForecast[]> GetWeatherForecasts()
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            ))
            .ToArray();

        return forecast;
    }
}
