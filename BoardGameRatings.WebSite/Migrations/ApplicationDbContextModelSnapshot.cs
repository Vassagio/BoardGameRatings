using BoardGameRatings.WebSite.Models;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;

namespace BoardGameRatings.WebSite.Migrations
{
    [DbContext(typeof (ApplicationDbContext))]
    internal class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BoardGameRatings.WebSite.Models.Game", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("Description")
                    .HasAnnotation("MaxLength", 4000);

                b.Property<string>("Name")
                    .IsRequired()
                    .HasAnnotation("MaxLength", 100);

                b.HasKey("Id")
                    .HasAnnotation("SqlServer:Clustered", false);

                b.HasIndex("Name")
                    .IsUnique();
            });

            modelBuilder.Entity("BoardGameRatings.WebSite.Models.Player", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("FirstName")
                    .IsRequired()
                    .HasAnnotation("MaxLength", 20);

                b.Property<string>("LastName")
                    .IsRequired()
                    .HasAnnotation("MaxLength", 30);

                b.HasKey("Id")
                    .HasAnnotation("SqlServer:Clustered", false);

                b.HasIndex("FirstName", "LastName")
                    .IsUnique();
            });
        }
    }
}