using Microsoft.EntityFrameworkCore.Migrations;

namespace byosWinContUpload.Migrations
{
    public partial class AddImgModelToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    imgURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    imgTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rzimgURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    wtimgURL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");
        }
    }
}
