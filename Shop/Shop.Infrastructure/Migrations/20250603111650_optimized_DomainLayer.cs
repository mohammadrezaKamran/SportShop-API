using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    public partial class optimized_DomainLayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "slider",
                table: "Sliders",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(120)",
                oldMaxLength: 120);

            migrationBuilder.AddColumn<string>(
                name: "AltText",
                schema: "slider",
                table: "Sliders",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "slider",
                table: "Sliders",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "slider",
                table: "Sliders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "slider",
                table: "Sliders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AltText",
                schema: "product",
                table: "Products",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AltText",
                schema: "product",
                table: "Images",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AltText",
                schema: "banner",
                table: "Banners",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "banner",
                table: "Banners",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "banner",
                table: "Banners",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "banner",
                table: "Banners",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "banner",
                table: "Banners",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AltText",
                schema: "slider",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "slider",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "slider",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "slider",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "AltText",
                schema: "product",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AltText",
                schema: "product",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "AltText",
                schema: "banner",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "banner",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "banner",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "banner",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "Title",
                schema: "banner",
                table: "Banners");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "slider",
                table: "Sliders",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);
        }
    }
}
