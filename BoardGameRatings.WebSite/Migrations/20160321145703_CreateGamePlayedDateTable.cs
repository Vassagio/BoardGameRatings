using System;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;

namespace BoardGameRatings.WebSite.Migrations
{
    public partial class CreateGamePlayedDateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_GameCategory_Category_CategoryId", "GameCategory");
            migrationBuilder.DropForeignKey("FK_GameCategory_Game_GameId", "GameCategory");
            migrationBuilder.DropForeignKey("FK_PlayerGame_Game_GameId", "PlayerGame");
            migrationBuilder.DropForeignKey("FK_PlayerGame_Player_PlayerId", "PlayerGame");
            migrationBuilder.CreateTable("GamePlayedDate", table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                GameId = table.Column<int>(nullable: false),
                PlayedDate = table.Column<DateTime>(nullable: false)
            },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlayedDate", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey("FK_GamePlayedDate_Game_GameId", x => x.GameId, "Game", "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateIndex("IX_GamePlayedDate_GameId_PlayedDate", "GamePlayedDate",
                new[] {"GameId", "PlayedDate"},
                unique: true)
                .Annotation("SqlServer:Clustered", true);
            migrationBuilder.AddForeignKey("FK_GameCategory_Category_CategoryId", "GameCategory", "CategoryId",
                "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey("FK_GameCategory_Game_GameId", "GameCategory", "GameId", "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey("FK_PlayerGame_Game_GameId", "PlayerGame", "GameId", "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey("FK_PlayerGame_Player_PlayerId", "PlayerGame", "PlayerId", "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_GameCategory_Category_CategoryId", "GameCategory");
            migrationBuilder.DropForeignKey("FK_GameCategory_Game_GameId", "GameCategory");
            migrationBuilder.DropForeignKey("FK_PlayerGame_Game_GameId", "PlayerGame");
            migrationBuilder.DropForeignKey("FK_PlayerGame_Player_PlayerId", "PlayerGame");
            migrationBuilder.DropTable("GamePlayedDate");
            migrationBuilder.AddForeignKey("FK_GameCategory_Category_CategoryId", "GameCategory", "CategoryId",
                "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_GameCategory_Game_GameId", "GameCategory", "GameId", "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_PlayerGame_Game_GameId", "PlayerGame", "GameId", "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey("FK_PlayerGame_Player_PlayerId", "PlayerGame", "PlayerId", "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}