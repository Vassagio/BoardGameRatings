using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;

namespace BoardGameRatings.WebSite.Migrations
{
    public partial class CreateGameTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable("Game", table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                Description = table.Column<string>(nullable: true),
                Name = table.Column<string>(nullable: false)
            },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.UniqueConstraint("AK_Game_Name", x => x.Name)
                        .Annotation("SqlServer:Clustered", true);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Game");
        }
    }
}