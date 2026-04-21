using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulgarianPhotoSpots.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDisplayNameToUserProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "UserProfiles");
        }
    }
}
