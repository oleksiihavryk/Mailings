using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mailings.Resources.Data.Migrations
{
    public partial class CascadeMailDeleting4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MailingHistory_MailingGroups_GroupId",
                table: "MailingHistory");

            migrationBuilder.AddForeignKey(
                name: "FK_MailingHistory_MailingGroups_GroupId",
                table: "MailingHistory",
                column: "GroupId",
                principalTable: "MailingGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MailingHistory_MailingGroups_GroupId",
                table: "MailingHistory");

            migrationBuilder.AddForeignKey(
                name: "FK_MailingHistory_MailingGroups_GroupId",
                table: "MailingHistory",
                column: "GroupId",
                principalTable: "MailingGroups",
                principalColumn: "Id");
        }
    }
}
