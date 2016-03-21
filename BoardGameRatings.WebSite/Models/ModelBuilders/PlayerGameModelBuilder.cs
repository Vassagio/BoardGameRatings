using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata.Builders;

namespace BoardGameRatings.WebSite.Models.ModelBuilders
{
    public class PlayerGameModelBuilder : IModelBuilder
    {
        private readonly EntityTypeBuilder<PlayerGame> _builder;

        public PlayerGameModelBuilder(EntityTypeBuilder<PlayerGame> builder)
        {
            _builder = builder;
        }

        public void Build()
        {
            _builder.HasKey(e => e.Id).ForSqlServerIsClustered(false);
            _builder.Property(e => e.Id).IsRequired().UseSqlServerIdentityColumn();
            _builder.HasIndex(e => new {e.PlayerId, e.GameId}).IsUnique().ForSqlServerIsClustered();
            _builder.Property(e => e.PlayerId).IsRequired();
            _builder.HasOne(e => e.Player).WithMany(e => e.Games).HasForeignKey(e => e.PlayerId);
            _builder.HasOne(e => e.Game).WithMany(e => e.Players).HasForeignKey(e => e.GameId);
            _builder.Property(e => e.GameId).IsRequired();
        }
    }
}