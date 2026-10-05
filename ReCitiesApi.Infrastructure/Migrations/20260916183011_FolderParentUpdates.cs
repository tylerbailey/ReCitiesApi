using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReCitiesApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FolderParentUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Folders_FolderId",
                table: "Folders");

            migrationBuilder.RenameColumn(
                name: "FolderId",
                table: "Folders",
                newName: "ParentId1");

            migrationBuilder.RenameIndex(
                name: "IX_Folders_FolderId",
                table: "Folders",
                newName: "IX_Folders_ParentId1");

            migrationBuilder.CreateIndex(
                name: "IX_Folders_ParentId",
                table: "Folders",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_Folders_ParentId",
                table: "Folders",
                column: "ParentId",
                principalTable: "Folders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_Folders_ParentId1",
                table: "Folders",
                column: "ParentId1",
                principalTable: "Folders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Folders_ParentId",
                table: "Folders");

            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Folders_ParentId1",
                table: "Folders");

            migrationBuilder.DropIndex(
                name: "IX_Folders_ParentId",
                table: "Folders");

            migrationBuilder.RenameColumn(
                name: "ParentId1",
                table: "Folders",
                newName: "FolderId");

            migrationBuilder.RenameIndex(
                name: "IX_Folders_ParentId1",
                table: "Folders",
                newName: "IX_Folders_FolderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_Folders_FolderId",
                table: "Folders",
                column: "FolderId",
                principalTable: "Folders",
                principalColumn: "Id");
        }
    }
}
