using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace REVOPS.DevChallenge.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaCamposContato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatInfos",
                table: "ChatInfos");

            migrationBuilder.AlterColumn<string>(
                name: "ChatId",
                table: "ChatInfos",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ChatInfos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "ChatInfos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactName",
                table: "ChatInfos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatInfos",
                table: "ChatInfos",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatInfos",
                table: "ChatInfos");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ChatInfos");

            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "ChatInfos");

            migrationBuilder.DropColumn(
                name: "ContactName",
                table: "ChatInfos");

            migrationBuilder.AlterColumn<string>(
                name: "ChatId",
                table: "ChatInfos",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatInfos",
                table: "ChatInfos",
                column: "ChatId");
        }
    }
}
