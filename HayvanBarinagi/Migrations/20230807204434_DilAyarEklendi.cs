using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HayvanBarinagi.Migrations
{
    public partial class DilAyarEklendi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sahiplendirildi",
                table: "Hayvanlar");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Sahiplendirildi",
                table: "Hayvanlar",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
