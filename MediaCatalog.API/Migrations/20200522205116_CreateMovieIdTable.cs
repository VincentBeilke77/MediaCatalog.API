using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaCatalog.API.Migrations
{
    public partial class CreateMovieIdTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IdentitySeed",
                columns: table => new
                {
                    TableName = table.Column<string>(maxLength: 25, nullable: false),
                    Seed = table.Column<int>(nullable: false),
                    IncrementValue = table.Column<int>(defaultValue: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentitySeed", x => x.TableName);
                });

            migrationBuilder.InsertData(
                table: "IdentitySeed",
                columns: new[] { "TableName", "Seed" },
                values: new object[] { "Movie", 1000 });
            migrationBuilder.InsertData(
                table: "IdentitySeed",
                columns: new[] { "TableName", "Seed" },
                values: new object[] { "Actor", 1000 });
            migrationBuilder.InsertData(
                table: "IdentitySeed",
                columns: new[] { "TableName", "Seed" },
                values: new object[] { "Director", 1000 });
            migrationBuilder.InsertData(
                table: "IdentitySeed",
                columns: new[] { "TableName", "Seed" },
                values: new object[] { "Genre", 1000 });
            migrationBuilder.InsertData(
                table: "IdentitySeed",
                columns: new[] { "TableName", "Seed" },
                values: new object[] { "MediaType", 1000 });
            migrationBuilder.InsertData(
                table: "IdentitySeed",
                columns: new[] { "TableName", "Seed" },
                values: new object[] { "Rating", 1000 });
            migrationBuilder.InsertData(
                table: "IdentitySeed",
                columns: new[] { "TableName", "Seed" },
                values: new object[] { "Studio", 1000 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentitySeed");
        }
    }
}