using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliothecaManager.Infrastructure.Migrations
{
    public partial class ModifiedBookItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookItemStatusId",
                table: "BookItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookItemStatusId",
                table: "BookItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
