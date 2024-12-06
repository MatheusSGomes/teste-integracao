using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using TestesIntegracao.Tests.Fixtures;

namespace TestesIntegracao.Tests.Factories;

[Collection("Database")]
public class WeatherFactory : WebApplicationFactory<Program>
{
    private readonly DbFixture _dbFixture;

    public WeatherFactory(DbFixture dbFixture)
    {
        _dbFixture = dbFixture;
    }

    /*
     * Qual ambiente será usado.
     * Quais configurações teria no appsettings
     * Faz a injeção de dependência se necessário.
     */
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");

        builder.ConfigureServices(services => { });

        builder.ConfigureAppConfiguration((context, configuration) =>
        {
            configuration.AddInMemoryCollection(new[]
            {
                new KeyValuePair<string, string?>("ConnectionStrings:TestesIntegracaoConn", _dbFixture.ConnectionString),
            });
        });
    }
}
