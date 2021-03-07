using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaCatalog.API.Migrations
{
    public partial class CreateMediaImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MediaImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageData = table.Column<byte[]>(nullable: false),
                    ImageName = table.Column<string>(maxLength: 25, nullable: false),
                    MovieId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaImages_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MediaImages_MovieId",
                table: "MediaImages",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MediaImages");
        }
    }
}
