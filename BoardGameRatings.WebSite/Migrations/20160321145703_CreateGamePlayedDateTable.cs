using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace BoardGameRatings.WebSite.Migrations
{
    public partial class CreateGamePlayedDateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_GameCategory_Category_CategoryId", table: "GameCategory");
            migrationBuilder.DropForeignKey(name: "FK_GameCategory_Game_GameId", table: "GameCategory");
            migrationBuilder.DropForeignKey(name: "FK_PlayerGame_Game_GameId", table: "PlayerGame");
            migrationBuilder.DropForeignKey(name: "FK_PlayerGame_Player_PlayerId", table: "PlayerGame");
            migrationBuilder.CreateTable(
                name: "GamePlayedDate",
                columns: table => new
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
                    table.ForeignKey(
                        name: "FK_GamePlayedDate_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateIndex(
                name: "IX_GamePlayedDate_GameId_PlayedDate",
                table: "GamePlayedDate",
                columns: new[] { "GameId", "PlayedDate" },
                unique: true)
                .Annotation("SqlServer:Clustered", true);
            migrationBuilder.AddForeignKey(
                name: "FK_GameCategory_Category_CategoryId",
                table: "GameCategory",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_GameCategory_Game_GameId",
                table: "GameCategory",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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
            migrationBuilder.DropForeignKey(name: "FK_GameCategory_Category_CategoryId", table: "GameCategory");
            migrationBuilder.DropForeignKey(name: "FK_GameCategory_Game_GameId", table: "GameCategory");
            migrationBuilder.DropForeignKey(name: "FK_PlayerGame_Game_GameId", table: "PlayerGame");
            migrationBuilder.DropForeignKey(name: "FK_PlayerGame_Player_PlayerId", table: "PlayerGame");
            migrationBuilder.DropTable("GamePlayedDate");
            migrationBuilder.AddForeignKey(
                name: "FK_GameCategory_Category_CategoryId",
                table: "GameCategory",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_GameCategory_Game_GameId",
                table: "GameCategory",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
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
