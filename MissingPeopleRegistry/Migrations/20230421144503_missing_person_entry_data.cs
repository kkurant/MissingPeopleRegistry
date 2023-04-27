using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MissingPeopleRegistry.Migrations
{
    public partial class missing_person_entry_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "MissingPeople",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyDate",
                table: "MissingPeople",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "MissingPeople");

            migrationBuilder.DropColumn(
                name: "ModifyDate",
                table: "MissingPeople");
        }
    }
}
