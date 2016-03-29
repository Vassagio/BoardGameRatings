using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata.Builders;

namespace BoardGameRatings.WebSite.Models.ModelBuilders
{
    public class GamePlayedDateModelBuilder : IModelBuilder
    {
        private readonly EntityTypeBuilder<GamePlayedDate> _builder;

        public GamePlayedDateModelBuilder(EntityTypeBuilder<GamePlayedDate> builder)
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
            _builder.HasIndex(e => new {e.GameId, e.PlayedDate})
                .IsUnique()
                .ForSqlServerIsClustered();
            _builder.Property(e => e.GameId)
                .IsRequired();
            _builder.HasOne(e => e.Game)
                .WithMany(e => e.PlayedDates)
                .HasForeignKey(e => e.GameId);
            _builder.Property(e => e.PlayedDate)
                .IsRequired();
        }
    }
}