using Microsoft.EntityFrameworkCore;
using TestesIntegracao.API.Data;

namespace TestesIntegracao.Tests.Fixtures;

public class DbFixture : IDisposable
{
    private readonly ApplicationDbContext _context;
    private bool _disposed;

    public readonly string DatabaseName = $"Context-{Guid.NewGuid()}";
    public readonly string ConnectionString;

    public DbFixture()
    {
        ConnectionString =
            $"Server=localhost,1433;Database={DatabaseName};User ID=sa;Password=1q2w3e4r@#$;Encrypt=False;";

        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        builder.UseSqlServer(ConnectionString);

        _context = new ApplicationDbContext(builder.Options);

        /* Sempre que iniciado os testes, será gerado um novo banco. Esse comando gera as migrations novamente.*/
        _context.Database.Migrate();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                /*
                    Sempre que um disposing for executado, a base será apagada.
                    Se os testes forem pausados (debug) durante o teste, o banco ficaria criado.
                 */
                _context.Database.EnsureDeleted();
            }

            _disposed = true;
        }
    }
}
