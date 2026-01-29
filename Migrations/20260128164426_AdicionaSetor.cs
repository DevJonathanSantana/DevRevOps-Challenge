using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace REVOPS.DevChallenge.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaSetor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sector",
                table: "ChatInfos",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sector",
                table: "ChatInfos");
        }
    }
}
