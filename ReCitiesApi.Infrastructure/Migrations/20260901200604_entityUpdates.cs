using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReCitiesApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class entityUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NeighborhoodId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NeighborhoodId",
                table: "AspNetUsers",
                column: "NeighborhoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Neighborhoods_NeighborhoodId",
                table: "AspNetUsers",
                column: "NeighborhoodId",
                principalTable: "Neighborhoods",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Neighborhoods_NeighborhoodId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_NeighborhoodId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NeighborhoodId",
                table: "AspNetUsers");
        }
    }
}
