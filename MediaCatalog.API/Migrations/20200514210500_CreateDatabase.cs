using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaCatalog.API.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 25, nullable: true),
                    LastName = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Directors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 25, nullable: true),
                    LastName = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 25, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MediaType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 25, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 25, nullable: false),
                    ShortDescription = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Studios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 25, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    ShortDescription = table.Column<string>(maxLength: 255, nullable: true),
                    LongDescription = table.Column<string>(nullable: true),
                    RunTime = table.Column<int>(nullable: false),
                    ReleaseYear = table.Column<int>(nullable: false),
                    Favorite = table.Column<bool>(nullable: false),
                    RatingId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_Ratings_RatingId",
                        column: x => x.RatingId,
                        principalTable: "Ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActorMovie",
                columns: table => new
                {
                    ActorId = table.Column<int>(nullable: false),
                    MovieId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorMovie", x => new { x.ActorId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_ActorMovie_Actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActorMovie_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DirectorMovie",
                columns: table => new
                {
                    DirectorId = table.Column<int>(nullable: false),
                    MovieId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectorMovie", x => new { x.DirectorId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_DirectorMovie_Directors_DirectorId",
                        column: x => x.DirectorId,
                        principalTable: "Directors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DirectorMovie_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GenreMovie",
                columns: table => new
                {
                    GenreId = table.Column<int>(nullable: false),
                    MovieId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreMovie", x => new { x.GenreId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_GenreMovie_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreMovie_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MediaTypeMovie",
                columns: table => new
                {
                    MediaTypeId = table.Column<int>(nullable: false),
                    MovieId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaTypeMovie", x => new { x.MediaTypeId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_MediaTypeMovie_MediaType_MediaTypeId",
                        column: x => x.MediaTypeId,
                        principalTable: "MediaType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MediaTypeMovie_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudioMovie",
                columns: table => new
                {
                    StudioId = table.Column<int>(nullable: false),
                    MovieId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudioMovie", x => new { x.StudioId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_StudioMovie_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudioMovie_Studios_StudioId",
                        column: x => x.StudioId,
                        principalTable: "Studios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Actors",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[] { 1, "Sylvester", "Stallone" });

            migrationBuilder.InsertData(
                table: "Directors",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[] { 1, "Sylvestor", "Stallone" });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "featuring characters involved in exciting and usually dangerous activities and adventures The movie is closer to an action-adventure thriller than a journalistic account, but energetic acting and vigorous directing make it work harrowingly well on its own terms.", "Action Adventure" });

            migrationBuilder.InsertData(
                table: "MediaType",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "a format of DVD designed for the storage of high-definition video and data.", "Blu-ray" });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "Description", "Name", "ShortDescription" },
                values: new object[] { 1, "Under 17 requires accompanying parent or adult guardian. Contains some adult material. Parents are urged to learn more about the film before taking their young children with them.", "R", null });

            migrationBuilder.InsertData(
                table: "Studios",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "The first major new studio in decades, Lionsgate is a global content leader with a reputation for innovation whose films, television series, location-based and live entertainment attractions and Starz premium pay platform reach next generation audiences around the world.", "Lionsgate" });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Favorite", "LongDescription", "RatingId", "ReleaseYear", "RunTime", "ShortDescription", "Title" },
                values: new object[] { 1, true, "After spending several years in Northern Thailand operating a longboat on the Salween River, John Rambo reluctantly agrees to carry a group of Christian missionaries into war-torn Burma. But when the aid workers are captured by ruthless Nationalist Army soldiers, Rambo leads a group of battle-scarred, combat-hardened mercenaries on an epic last-ditch mission to rescue the prisoners - at all costs.", 1, 2008, 91, "The ultimate American action hero returns - with a vengeance!", "Rambo: The Fight Continues" });

            migrationBuilder.InsertData(
                table: "ActorMovie",
                columns: new[] { "ActorId", "MovieId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "DirectorMovie",
                columns: new[] { "DirectorId", "MovieId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "GenreMovie",
                columns: new[] { "GenreId", "MovieId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "MediaTypeMovie",
                columns: new[] { "MediaTypeId", "MovieId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "StudioMovie",
                columns: new[] { "StudioId", "MovieId" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_ActorMovie_MovieId",
                table: "ActorMovie",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectorMovie_MovieId",
                table: "DirectorMovie",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_GenreMovie_MovieId",
                table: "GenreMovie",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaTypeMovie_MovieId",
                table: "MediaTypeMovie",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_RatingId",
                table: "Movies",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_StudioMovie_MovieId",
                table: "StudioMovie",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActorMovie");

            migrationBuilder.DropTable(
                name: "DirectorMovie");

            migrationBuilder.DropTable(
                name: "GenreMovie");

            migrationBuilder.DropTable(
                name: "MediaTypeMovie");

            migrationBuilder.DropTable(
                name: "StudioMovie");

            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "Directors");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "MediaType");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Studios");

            migrationBuilder.DropTable(
                name: "Ratings");
        }
    }
}
