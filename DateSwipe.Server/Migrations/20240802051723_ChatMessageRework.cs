﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DateSwipe.Server.Migrations
{
    /// <inheritdoc />
    public partial class ChatMessageRework : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TimeStamp",
                table: "UserSwipes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "ChatMessages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "UserSwipes");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "ChatMessages");
        }
    }
}
