using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolWebsite.Migrations
{
    /// <inheritdoc />
    public partial class GalleryUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Gallries",
                table: "Gallries");

            migrationBuilder.RenameTable(
                name: "Gallries",
                newName: "Galleries");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Galleries",
                table: "Galleries",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Galleries",
                table: "Galleries");

            migrationBuilder.RenameTable(
                name: "Galleries",
                newName: "Gallries");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gallries",
                table: "Gallries",
                column: "Id");
        }
    }
}
