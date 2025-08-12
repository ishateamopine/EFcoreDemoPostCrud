using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFcoreDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddBlogStoredProcs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlogType",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "RssUrl",
                table: "Blogs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BlogType",
                table: "Blogs",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RssUrl",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
