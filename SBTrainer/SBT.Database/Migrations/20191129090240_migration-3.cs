using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SBT.Database.Migrations
{
    public partial class migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Sites",
                table: "Sites");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Sites");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Sites",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sites",
                table: "Sites",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Sites",
                table: "Sites");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Sites");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Sites",
                type: "varchar(10)")
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sites",
                table: "Sites",
                column: "Id");
        }
    }
}
