using CQRS.Cadastro.Domain.Objects;
using CQRS.Core.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS.Cadastro.Data
{
    public class CadastroContext : DbContext, IUnitOfWork
    {
        public CadastroContext(DbContextOptions<CadastroContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Contato> Contatos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder = MapearPropriedadesEsquecidas(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CadastroContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }

        private ModelBuilder MapearPropriedadesEsquecidas(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            return modelBuilder;
        }
    }
}
