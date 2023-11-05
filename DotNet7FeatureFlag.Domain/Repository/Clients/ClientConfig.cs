using DotNet7FeatureFlag.App.Domain.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DotNet7FeatureFlag.App.Repository.Clients
{
    public class ClientConfig : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("clients");
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.Value)
                .HasColumnName("value")
                .IsRequired();

            builder
                .Property(x => x.Description)
                .HasColumnName("description");

            builder
                .Property(x => x.LastAcess)
                .HasColumnName("lastacess")
                .IsRequired();
        }
    }
}
