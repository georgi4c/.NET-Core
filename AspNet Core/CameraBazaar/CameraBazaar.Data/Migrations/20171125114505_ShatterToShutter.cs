using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CameraBazaar.Web.Data.Migrations
{
    public partial class ShatterToShutter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MinShatterSpeed",
                table: "Cameras",
                newName: "MinShutterSpeed");

            migrationBuilder.RenameColumn(
                name: "MaxShatterSpeed",
                table: "Cameras",
                newName: "MaxShutterSpeed");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MinShutterSpeed",
                table: "Cameras",
                newName: "MinShatterSpeed");

            migrationBuilder.RenameColumn(
                name: "MaxShutterSpeed",
                table: "Cameras",
                newName: "MaxShatterSpeed");
        }
    }
}
