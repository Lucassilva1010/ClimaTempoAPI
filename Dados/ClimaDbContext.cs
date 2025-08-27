using Microsoft.EntityFrameworkCore;
using ClimaApi.Modelos;

namespace ClimaApi.Dados
{
    public class ClimaDbContext : DbContext
    {
        public ClimaDbContext(DbContextOptions<ClimaDbContext> options) : base(options)
        {
        }

        // Essa propriedade vai virar a tabela RegistrosClima no banco
        public DbSet<RegistroClima> RegistrosClima { get; set; } = null!;
    }
}
