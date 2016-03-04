using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace BoardGameRatings.WebSite.Migrations
{
    public partial class CreateGameCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_PlayerGame_Game_GameId", table: "PlayerGame");
            migrationBuilder.DropForeignKey(name: "FK_PlayerGame_Player_PlayerId", table: "PlayerGame");
            migrationBuilder.CreateTable(
                name: "GameCategory",
                columns: table => new
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
                    table.ForeignKey(
                        name: "FK_GameCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameCategory_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateIndex(
                name: "IX_GameCategory_GameId_CategoryId",
                table: "GameCategory",
                columns: new[] { "GameId", "CategoryId" },
                unique: true)
                .Annotation("SqlServer:Clustered", true);
            migrationBuilder.AddForeignKey(
                name: "FK_PlayerGame_Game_GameId",
                table: "PlayerGame",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_PlayerGame_Player_PlayerId",
                table: "PlayerGame",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_PlayerGame_Game_GameId", table: "PlayerGame");
            migrationBuilder.DropForeignKey(name: "FK_PlayerGame_Player_PlayerId", table: "PlayerGame");
            migrationBuilder.DropTable("GameCategory");
            migrationBuilder.AddForeignKey(
                name: "FK_PlayerGame_Game_GameId",
                table: "PlayerGame",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_PlayerGame_Player_PlayerId",
                table: "PlayerGame",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
