using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixClientNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "phone",
                table: "Clients",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "fullName",
                table: "Clients",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Clients",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "fullName",
                table: "Agents",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Agents",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Agents",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Clients",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Clients",
                newName: "fullName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Clients",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Agents",
                newName: "fullName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Agents",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Agents",
                newName: "id");
        }
    }
}
