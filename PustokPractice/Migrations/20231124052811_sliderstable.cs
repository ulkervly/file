using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PustokPractice.Migrations
{
    public partial class sliderstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrL",
                table: "Sliders",
                newName: "Image");

            migrationBuilder.AddColumn<string>(
                name: "ButtonText",
                table: "Sliders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ButtonText",
                table: "Sliders");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Sliders",
                newName: "ImageUrL");
        }
    }
}
