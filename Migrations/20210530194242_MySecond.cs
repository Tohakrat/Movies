using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcMovie.Migrations
{
    public partial class MySecond : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Genres_GenreId1",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Producers_producerIdId",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "producerIdId",
                table: "Movies",
                newName: "ProducerId");

            migrationBuilder.RenameColumn(
                name: "GenreId1",
                table: "Movies",
                newName: "GenreId");

            migrationBuilder.RenameIndex(
                name: "IX_Movies_producerIdId",
                table: "Movies",
                newName: "IX_Movies_ProducerId");

            migrationBuilder.RenameIndex(
                name: "IX_Movies_GenreId1",
                table: "Movies",
                newName: "IX_Movies_GenreId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReleaseDate",
                table: "Movies",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Genres_GenreId",
                table: "Movies",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Producers_ProducerId",
                table: "Movies",
                column: "ProducerId",
                principalTable: "Producers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Genres_GenreId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Producers_ProducerId",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "ProducerId",
                table: "Movies",
                newName: "producerIdId");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "Movies",
                newName: "GenreId1");

            migrationBuilder.RenameIndex(
                name: "IX_Movies_ProducerId",
                table: "Movies",
                newName: "IX_Movies_producerIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Movies_GenreId",
                table: "Movies",
                newName: "IX_Movies_GenreId1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReleaseDate",
                table: "Movies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Genres_GenreId1",
                table: "Movies",
                column: "GenreId1",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Producers_producerIdId",
                table: "Movies",
                column: "producerIdId",
                principalTable: "Producers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
