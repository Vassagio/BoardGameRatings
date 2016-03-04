using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata.Builders;

namespace BoardGameRatings.WebSite.Models.ModelBuilders
{
    public class CategoryModelBuilder
    {
        private readonly EntityTypeBuilder<Category> _builder;

        public CategoryModelBuilder(EntityTypeBuilder<Category> builder)
        {
            _builder = builder;
        }

        public void Build()
        {
            _builder.HasKey(e => e.Id).ForSqlServerIsClustered(false);
            _builder.Property(e => e.Id).IsRequired().UseSqlServerIdentityColumn();
            _builder.HasIndex(u => u.Description).IsUnique();
            _builder.Property(e => e.Description).IsRequired().HasMaxLength(50);
        }
    }
}