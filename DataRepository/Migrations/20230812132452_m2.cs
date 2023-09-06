using Microsoft.EntityFrameworkCore.Migrations;

namespace DataRepository.Migrations
{
    public partial class m2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "backWood",
                table: "guitars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "builder",
                table: "guitars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "model",
                table: "guitars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "price",
                table: "guitars",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "topWood",
                table: "guitars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "guitars",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "backWood",
                table: "guitars");

            migrationBuilder.DropColumn(
                name: "builder",
                table: "guitars");

            migrationBuilder.DropColumn(
                name: "model",
                table: "guitars");

            migrationBuilder.DropColumn(
                name: "price",
                table: "guitars");

            migrationBuilder.DropColumn(
                name: "topWood",
                table: "guitars");

            migrationBuilder.DropColumn(
                name: "type",
                table: "guitars");
        }
    }
}
