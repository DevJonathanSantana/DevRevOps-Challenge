using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace REVOPS.DevChallenge.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaNomeAtendente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AgentName",
                table: "ChatInfos",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgentName",
                table: "ChatInfos");
        }
    }
}
