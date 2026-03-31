using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulgarianPhotoSpots.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixFavorites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorite_PhotoSpots_PhotoSpotId",
                table: "Favorite");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorite",
                table: "Favorite");

            migrationBuilder.RenameTable(
                name: "Favorite",
                newName: "Favorites");

            migrationBuilder.RenameIndex(
                name: "IX_Favorite_PhotoSpotId",
                table: "Favorites",
                newName: "IX_Favorites_PhotoSpotId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_PhotoSpots_PhotoSpotId",
                table: "Favorites",
                column: "PhotoSpotId",
                principalTable: "PhotoSpots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_PhotoSpots_PhotoSpotId",
                table: "Favorites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites");

            migrationBuilder.RenameTable(
                name: "Favorites",
                newName: "Favorite");

            migrationBuilder.RenameIndex(
                name: "IX_Favorites_PhotoSpotId",
                table: "Favorite",
                newName: "IX_Favorite_PhotoSpotId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorite",
                table: "Favorite",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorite_PhotoSpots_PhotoSpotId",
                table: "Favorite",
                column: "PhotoSpotId",
                principalTable: "PhotoSpots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
