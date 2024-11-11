using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResearchManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class accmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Branch_Campus",
                table: "Production",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Keyword1",
                table: "Production",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Keyword2",
                table: "Production",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Keyword3",
                table: "Production",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Keyword4",
                table: "Production",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Keyword5",
                table: "Production",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Keyword6",
                table: "Production",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Keyword7",
                table: "Production",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RequiredFile3Data",
                table: "Production",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequiredFile3Name",
                table: "Production",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeofFunding",
                table: "Production",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeofResearch",
                table: "Production",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Production",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Branch_Campus",
                table: "Production");

            migrationBuilder.DropColumn(
                name: "Keyword1",
                table: "Production");

            migrationBuilder.DropColumn(
                name: "Keyword2",
                table: "Production");

            migrationBuilder.DropColumn(
                name: "Keyword3",
                table: "Production");

            migrationBuilder.DropColumn(
                name: "Keyword4",
                table: "Production");

            migrationBuilder.DropColumn(
                name: "Keyword5",
                table: "Production");

            migrationBuilder.DropColumn(
                name: "Keyword6",
                table: "Production");

            migrationBuilder.DropColumn(
                name: "Keyword7",
                table: "Production");

            migrationBuilder.DropColumn(
                name: "RequiredFile3Data",
                table: "Production");

            migrationBuilder.DropColumn(
                name: "RequiredFile3Name",
                table: "Production");

            migrationBuilder.DropColumn(
                name: "TypeofFunding",
                table: "Production");

            migrationBuilder.DropColumn(
                name: "TypeofResearch",
                table: "Production");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Production");
        }
    }
}
