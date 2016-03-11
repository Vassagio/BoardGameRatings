using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;

namespace BoardGameRatings.WebSite.Migrations
{
    public partial class CreateGameCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_PlayerGame_Game_GameId", "PlayerGame");
            migrationBuilder.DropForeignKey("FK_PlayerGame_Player_PlayerId", "PlayerGame");
            migrationBuilder.CreateTable("GameCategory", table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                CategoryId = table.Column<int>(nullable: false),
                GameId = table.Column<int>(nullable: false)
            },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCategory", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey("FK_GameCategory_Category_CategoryId", x => x.CategoryId, "Category", "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey("FK_GameCategory_Game_GameId", x => x.GameId, "Game", "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateIndex("IX_GameCategory_GameId_CategoryId", "GameCategory",
                new[] {"GameId", "CategoryId"},
                unique: true)
                .Annotation("SqlServer:Clustered", true);
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
            migrationBuilder.DropTable("GameCategory");
            migrationBuilder.AddForeignKey("FK_PlayerGame_Game_GameId", "PlayerGame", "GameId", "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_PlayerGame_Player_PlayerId", "PlayerGame", "PlayerId", "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}