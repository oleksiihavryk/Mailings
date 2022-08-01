using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mailings.Resources.Data.Migrations
{
    public partial class Init9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailAddressFrom_MailingGroups_Id",
                table: "EmailAddressFrom");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailAddressFrom_MailingGroups_Id",
                table: "EmailAddressFrom",
                column: "Id",
                principalTable: "MailingGroups",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailAddressFrom_MailingGroups_Id",
                table: "EmailAddressFrom");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailAddressFrom_MailingGroups_Id",
                table: "EmailAddressFrom",
                column: "Id",
                principalTable: "MailingGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
