using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrindhouseGym.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTrainerToMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrainerId",
                table: "Members",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_TrainerId",
                table: "Members",
                column: "TrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Trainers_TrainerId",
                table: "Members",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "TrainerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Trainers_TrainerId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_TrainerId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "TrainerId",
                table: "Members");
        }
    }
}
