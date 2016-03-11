using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;

namespace BoardGameRatings.WebSite.Migrations
{
    public partial class CreateCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_PlayerGame_Game_GameId", "PlayerGame");
            migrationBuilder.DropForeignKey("FK_PlayerGame_Player_PlayerId", "PlayerGame");
            migrationBuilder.CreateTable("Category", table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                Description = table.Column<string>(nullable: false)
            },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });
            migrationBuilder.CreateIndex("IX_Category_Description", "Category", "Description",
                unique: true);
            migrationBuilder.AddForeignKey("FK_PlayerGame_Game_GameId", "PlayerGame", "GameId", "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey("FK_PlayerGame_Player_PlayerId", "PlayerGame", "PlayerId", "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_PlayerGame_Game_GameId", "PlayerGame");
            migrationBuilder.DropForeignKey("FK_PlayerGame_Player_PlayerId", "PlayerGame");
            migrationBuilder.DropTable("Category");
            migrationBuilder.AddForeignKey("FK_PlayerGame_Game_GameId", "PlayerGame", "GameId", "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_PlayerGame_Player_PlayerId", "PlayerGame", "PlayerId", "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}