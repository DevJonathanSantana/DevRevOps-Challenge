using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace REVOPS.DevChallenge.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "ChatInfos",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tags",
                table: "ChatInfos");
        }
    }
}
