using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mailings.Resources.Data.Migrations
{
    public partial class CascadeMailDeleting2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailReceivers_MailingGroups_GroupId",
                table: "EmailReceivers");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailReceivers_MailingGroups_GroupId",
                table: "EmailReceivers",
                column: "GroupId",
                principalTable: "MailingGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailReceivers_MailingGroups_GroupId",
                table: "EmailReceivers");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailReceivers_MailingGroups_GroupId",
                table: "EmailReceivers",
                column: "GroupId",
                principalTable: "MailingGroups",
                principalColumn: "Id");
        }
    }
}
