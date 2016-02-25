using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata.Builders;

namespace BoardGameRatings.WebSite.Models.ModelBuilders
{
    public class GameModelBuilder
    {
        private readonly EntityTypeBuilder<Game> _builder;

        public GameModelBuilder(EntityTypeBuilder<Game> builder)
        {
            _builder = builder;
        }

        public void Build()
        {
            _builder.HasKey(e => e.Id).ForSqlServerIsClustered(false);
            _builder.HasAlternateKey(e => e.Name).ForSqlServerIsClustered(true);
            _builder.Property(e => e.Id).IsRequired().UseSqlServerIdentityColumn();
            _builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
            _builder.Property(e => e.Description).HasMaxLength(4000);
        }
    }
}
