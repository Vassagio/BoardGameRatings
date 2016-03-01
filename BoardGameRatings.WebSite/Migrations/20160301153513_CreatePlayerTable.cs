using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;

namespace BoardGameRatings.WebSite.Migrations
{
    public partial class CreatePlayerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable("Player", table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                FirstName = table.Column<string>(nullable: false),
                LastName = table.Column<string>(nullable: false)
            },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });
            migrationBuilder.CreateIndex("IX_Player_FirstName_LastName", "Player", new[] {"FirstName", "LastName"},
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Player");
        }
    }
}