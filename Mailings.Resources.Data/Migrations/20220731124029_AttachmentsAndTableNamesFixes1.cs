using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mailings.Resources.Data.Migrations
{
    public partial class AttachmentsAndTableNamesFixes1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailAddressFrom_EmailAddress_AddressId",
                table: "EmailAddressFrom");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailAddressFrom_MailingGroups_GroupId",
                table: "EmailAddressFrom");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailAddressTo_EmailAddress_AddressId",
                table: "EmailAddressTo");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailAddressTo_MailingGroups_GroupId",
                table: "EmailAddressTo");

            migrationBuilder.DropForeignKey(
                name: "FK_MailingGroups_EmailAddressFrom_Id",
                table: "MailingGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailAddressTo",
                table: "EmailAddressTo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailAddressFrom",
                table: "EmailAddressFrom");

            migrationBuilder.RenameTable(
                name: "EmailAddressTo",
                newName: "EmailReceivers");

            migrationBuilder.RenameTable(
                name: "EmailAddressFrom",
                newName: "EmailSenders");

            migrationBuilder.RenameIndex(
                name: "IX_EmailAddressTo_GroupId",
                table: "EmailReceivers",
                newName: "IX_EmailReceivers_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_EmailAddressTo_AddressId",
                table: "EmailReceivers",
                newName: "IX_EmailReceivers_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_EmailAddressFrom_GroupId",
                table: "EmailSenders",
                newName: "IX_EmailSenders_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_EmailAddressFrom_AddressId",
                table: "EmailSenders",
                newName: "IX_EmailSenders_AddressId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "EmailSenders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailReceivers",
                table: "EmailReceivers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailSenders",
                table: "EmailSenders",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachment_Mail_MailId",
                        column: x => x.MailId,
                        principalTable: "Mail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_MailId",
                table: "Attachment",
                column: "MailId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailReceivers_EmailAddress_AddressId",
                table: "EmailReceivers",
                column: "AddressId",
                principalTable: "EmailAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailReceivers_MailingGroups_GroupId",
                table: "EmailReceivers",
                column: "GroupId",
                principalTable: "MailingGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailSenders_EmailAddress_AddressId",
                table: "EmailSenders",
                column: "AddressId",
                principalTable: "EmailAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailSenders_MailingGroups_GroupId",
                table: "EmailSenders",
                column: "GroupId",
                principalTable: "MailingGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MailingGroups_EmailSenders_Id",
                table: "MailingGroups",
                column: "Id",
                principalTable: "EmailSenders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailReceivers_EmailAddress_AddressId",
                table: "EmailReceivers");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailReceivers_MailingGroups_GroupId",
                table: "EmailReceivers");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailSenders_EmailAddress_AddressId",
                table: "EmailSenders");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailSenders_MailingGroups_GroupId",
                table: "EmailSenders");

            migrationBuilder.DropForeignKey(
                name: "FK_MailingGroups_EmailSenders_Id",
                table: "MailingGroups");

            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailSenders",
                table: "EmailSenders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailReceivers",
                table: "EmailReceivers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "EmailSenders");

            migrationBuilder.RenameTable(
                name: "EmailSenders",
                newName: "EmailAddressFrom");

            migrationBuilder.RenameTable(
                name: "EmailReceivers",
                newName: "EmailAddressTo");

            migrationBuilder.RenameIndex(
                name: "IX_EmailSenders_GroupId",
                table: "EmailAddressFrom",
                newName: "IX_EmailAddressFrom_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_EmailSenders_AddressId",
                table: "EmailAddressFrom",
                newName: "IX_EmailAddressFrom_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_EmailReceivers_GroupId",
                table: "EmailAddressTo",
                newName: "IX_EmailAddressTo_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_EmailReceivers_AddressId",
                table: "EmailAddressTo",
                newName: "IX_EmailAddressTo_AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailAddressFrom",
                table: "EmailAddressFrom",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailAddressTo",
                table: "EmailAddressTo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailAddressFrom_EmailAddress_AddressId",
                table: "EmailAddressFrom",
                column: "AddressId",
                principalTable: "EmailAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailAddressFrom_MailingGroups_GroupId",
                table: "EmailAddressFrom",
                column: "GroupId",
                principalTable: "MailingGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailAddressTo_EmailAddress_AddressId",
                table: "EmailAddressTo",
                column: "AddressId",
                principalTable: "EmailAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailAddressTo_MailingGroups_GroupId",
                table: "EmailAddressTo",
                column: "GroupId",
                principalTable: "MailingGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MailingGroups_EmailAddressFrom_Id",
                table: "MailingGroups",
                column: "Id",
                principalTable: "EmailAddressFrom",
                principalColumn: "Id");
        }
    }
}
