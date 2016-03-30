using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata.Builders;

namespace BoardGameRatings.WebSite.Models.ModelBuilders
{
    public class PlayerModelBuilder : IModelBuilder
    {
        private readonly EntityTypeBuilder<Player> _builder;

        public PlayerModelBuilder(EntityTypeBuilder<Player> builder)
        {
            _builder = builder;
        }

        public void Build()
        {
            _builder.HasKey(e => e.Id)
                .ForSqlServerIsClustered(false);
            _builder.Property(e => e.Id)
                .IsRequired()
                .UseSqlServerIdentityColumn();
            _builder.HasIndex(u => new {u.FirstName, u.LastName})
                .IsUnique();
            _builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(20);
            _builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(30);
        }
    }
}