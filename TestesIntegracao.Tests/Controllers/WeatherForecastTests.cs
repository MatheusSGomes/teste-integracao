using System.Net;
using System.Net.Http.Json;
using TestesIntegracao.API;
using TestesIntegracao.Tests.Factories;

namespace TestesIntegracao.Tests.Controllers;

[Collection("Database")]
public class WeatherForecastTests : IClassFixture<WeatherFactory>
{
    private readonly WeatherFactory _factory;

    public WeatherForecastTests(WeatherFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task CreateWeatherForecast_ShowReturn_Ok()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var request = new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
            Summary = "Teste",
            TemperatureC = 3
        };

        var response = await client.PostAsJsonAsync("WeatherForecast", request);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
