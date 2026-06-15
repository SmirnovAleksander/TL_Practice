using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class rename_pk_to_id_plus_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "RoomType",
                newName: "room_type_id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Reservation",
                newName: "reservation_id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Property",
                newName: "property_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "room_type_id",
                table: "RoomType",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "reservation_id",
                table: "Reservation",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "property_id",
                table: "Property",
                newName: "id");
        }
    }
}
