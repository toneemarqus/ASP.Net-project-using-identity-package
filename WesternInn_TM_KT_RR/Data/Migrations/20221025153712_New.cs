﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WesternInn_TM_KT_RR.Data.Migrations
{
    public partial class New : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "datediff",
                table: "Booking",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "datediff",
                table: "Booking");
        }
    }
}
