using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeriesBoxd.Migrations
{
    /// <inheritdoc />
    public partial class ClassesFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerieName",
                table: "Season");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SerieName",
                table: "Season",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
