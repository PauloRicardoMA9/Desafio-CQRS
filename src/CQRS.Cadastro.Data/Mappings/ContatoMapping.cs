using CQRS.Cadastro.Domain.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CQRS.Cadastro.Data.Mappings
{
    public class ContatoMapping : IEntityTypeConfiguration<Contato>
    {
        public void Configure(EntityTypeBuilder<Contato> builder)
        {
            builder.HasKey(contato => contato.Id);

            builder.Property(contato => contato.Ddd)
                .IsRequired()
                .HasColumnType("varchar(2)");

            builder.Property(contato => contato.Telefone)
                .IsRequired()
                .HasColumnType("varchar(9)");

            builder.Property(contato => contato.Email)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.ToTable("Contatos");
        }
    }
}
