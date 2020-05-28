using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaCatalog.API.Migrations
{
    public partial class CreateSPGetIdentitySeedForTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE GetIdentitySeedForTable
						   @Id int = 0,
						   @TableName varchar(MAX)
					   AS
					   BEGIN
						   SELECT @Id = Seed
							 FROM IdentitySeed
							WHERE TableName = @TableName

						   UPDATE IdentitySeed
							  SET Seed = Seed + IncrementValue
							WHERE TableName = @TableName

						   SELECT @Id AS Id
					   END;";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}