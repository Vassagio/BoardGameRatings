using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata.Builders;

namespace BoardGameRatings.WebSite.Models.ModelBuilders
{
    public class GameCategoryModelBuilder
    {
        private readonly EntityTypeBuilder<GameCategory> _builder;

        public GameCategoryModelBuilder(EntityTypeBuilder<GameCategory> builder)
        {
            _builder = builder;
        }

        public void Build()
        {
            _builder.HasKey(e => e.Id).ForSqlServerIsClustered(false);
            _builder.Property(e => e.Id).IsRequired().UseSqlServerIdentityColumn();
            _builder.HasIndex(e => new {e.GameId, e.CategoryId}).IsUnique().ForSqlServerIsClustered();
            _builder.Property(e => e.CategoryId).IsRequired();
            _builder.HasOne(e => e.Category).WithMany(e => e.Games).HasForeignKey(e => e.CategoryId);
            _builder.HasOne(e => e.Game).WithMany(e => e.Categories).HasForeignKey(e => e.GameId);
            _builder.Property(e => e.GameId).IsRequired();
        }
    }
}