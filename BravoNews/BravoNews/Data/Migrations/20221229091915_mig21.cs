using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BravoNews.Data.Migrations
{
    public partial class mig21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "WeatherForecast");

            migrationBuilder.AlterColumn<int>(
                name: "WindSpeed",
                table: "WeatherForecast",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "TemperatureF",
                table: "WeatherForecast",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "TemperatureC",
                table: "WeatherForecast",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "Humidity",
                table: "WeatherForecast",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<bool>(
                name: "PersonalizedNewsletter",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonalizedNewsletter",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<double>(
                name: "WindSpeed",
                table: "WeatherForecast",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "TemperatureF",
                table: "WeatherForecast",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "TemperatureC",
                table: "WeatherForecast",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "Humidity",
                table: "WeatherForecast",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "WeatherForecast",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
