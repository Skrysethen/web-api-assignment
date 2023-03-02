using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_api_assignment.Migrations
{
    /// <inheritdoc />
    public partial class NewMovieAndNewCharacter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Character",
                columns: new[] { "Id", "Alias", "FullName", "Gender", "PictureUrl" },
                values: new object[] { 4, "Bruce Wayne", "Batman", "m", null });

            migrationBuilder.UpdateData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 3,
                column: "Director",
                value: "Christopher Nolan");

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "Id", "Director", "FranchiseId", "Genre", "MovieTitle", "PictureURL", "ReleaseYear", "TrailerUrl" },
                values: new object[] { 4, "Christopher Nolan", 1, "Superhero", "Batman begins", null, 0, null });

            migrationBuilder.InsertData(
                table: "CharacterMovie",
                columns: new[] { "CharactersId", "MoviesId" },
                values: new object[] { 4, 4 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CharacterMovie",
                keyColumns: new[] { "CharactersId", "MoviesId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "Character",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 3,
                column: "Director",
                value: "who knows");
        }
    }
}
