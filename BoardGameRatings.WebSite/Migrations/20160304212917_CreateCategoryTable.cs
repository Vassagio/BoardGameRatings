using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace BoardGameRatings.WebSite.Migrations
{
    public partial class CreateCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_PlayerGame_Game_GameId", table: "PlayerGame");
            migrationBuilder.DropForeignKey(name: "FK_PlayerGame_Player_PlayerId", table: "PlayerGame");
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
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
            migrationBuilder.CreateIndex(
                name: "IX_Category_Description",
                table: "Category",
                column: "Description",
                unique: true);
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
            migrationBuilder.DropTable("Category");
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
