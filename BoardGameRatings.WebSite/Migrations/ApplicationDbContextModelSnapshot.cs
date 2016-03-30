using System;
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

            modelBuilder.Entity("BoardGameRatings.WebSite.Models.Category", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("Description")
                    .IsRequired()
                    .HasAnnotation("MaxLength", 50);

                b.HasKey("Id")
                    .HasAnnotation("SqlServer:Clustered", false);

                b.HasIndex("Description")
                    .IsUnique();
            });

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

            modelBuilder.Entity("BoardGameRatings.WebSite.Models.GameCategory", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<int>("CategoryId");

                b.Property<int>("GameId");

                b.HasKey("Id")
                    .HasAnnotation("SqlServer:Clustered", false);

                b.HasIndex("GameId", "CategoryId")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);
            });

            modelBuilder.Entity("BoardGameRatings.WebSite.Models.GamePlayedDate", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<int>("GameId");

                b.Property<DateTime>("PlayedDate");

                b.HasKey("Id")
                    .HasAnnotation("SqlServer:Clustered", false);

                b.HasIndex("GameId", "PlayedDate")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);
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

            modelBuilder.Entity("BoardGameRatings.WebSite.Models.PlayerGame", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<int>("GameId");

                b.Property<int>("PlayerId");

                b.HasKey("Id")
                    .HasAnnotation("SqlServer:Clustered", false);

                b.HasIndex("PlayerId", "GameId")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);
            });

            modelBuilder.Entity("BoardGameRatings.WebSite.Models.GameCategory", b =>
            {
                b.HasOne("BoardGameRatings.WebSite.Models.Category")
                    .WithMany()
                    .HasForeignKey("CategoryId");

                b.HasOne("BoardGameRatings.WebSite.Models.Game")
                    .WithMany()
                    .HasForeignKey("GameId");
            });

            modelBuilder.Entity("BoardGameRatings.WebSite.Models.GamePlayedDate", b =>
            {
                b.HasOne("BoardGameRatings.WebSite.Models.Game")
                    .WithMany()
                    .HasForeignKey("GameId");
            });

            modelBuilder.Entity("BoardGameRatings.WebSite.Models.PlayerGame", b =>
            {
                b.HasOne("BoardGameRatings.WebSite.Models.Game")
                    .WithMany()
                    .HasForeignKey("GameId");

                b.HasOne("BoardGameRatings.WebSite.Models.Player")
                    .WithMany()
                    .HasForeignKey("PlayerId");
            });
        }
    }
}