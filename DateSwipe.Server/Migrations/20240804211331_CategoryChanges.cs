﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DateSwipe.Server.Migrations
{
    /// <inheritdoc />
    public partial class CategoryChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DateIdeaCategories_Categories_CategoryId",
                table: "DateIdeaCategories");

            migrationBuilder.AddForeignKey(
                name: "FK_DateIdeaCategories_Categories_CategoryId",
                table: "DateIdeaCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DateIdeaCategories_Categories_CategoryId",
                table: "DateIdeaCategories");

            migrationBuilder.AddForeignKey(
                name: "FK_DateIdeaCategories_Categories_CategoryId",
                table: "DateIdeaCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
