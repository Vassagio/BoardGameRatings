using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using BoardGameRatings.WebSite.Models;

namespace BoardGameRatings.WebSite.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20160304144544_CreateGameTypeTable")]
    partial class CreateGameTypeTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("BoardGameRatings.WebSite.Models.GameType", b =>
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
