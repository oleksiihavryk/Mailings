﻿// <auto-generated />
using System;
using Mailings.Resources.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Mailings.Resources.Data.Migrations
{
    [DbContext(typeof(CommonResourcesDbContext))]
    [Migration("20220803093553_UserMails")]
    partial class UserMails
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Mailings.Resources.Domen.MailingService.HistoryNoteMailingGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsSucceded")
                        .HasColumnType("bit");

                    b.Property<DateTime>("When")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("MailingHistory");
                });

            modelBuilder.Entity("Mailings.Resources.Domen.Models.Attachment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("MailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MailId");

                    b.ToTable("Attachment");
                });

            modelBuilder.Entity("Mailings.Resources.Domen.Models.EmailAddress", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EmailAddress");
                });

            modelBuilder.Entity("Mailings.Resources.Domen.Models.EmailAddressFrom", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("GroupId");

                    b.ToTable("EmailSenders", (string)null);
                });

            modelBuilder.Entity("Mailings.Resources.Domen.Models.EmailAddressTo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("GroupId");

                    b.ToTable("EmailReceivers", (string)null);
                });

            modelBuilder.Entity("Mailings.Resources.Domen.Models.Mail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Theme")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Mail");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Mail");
                });

            modelBuilder.Entity("Mailings.Resources.Domen.Models.MailingGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("MailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MailId")
                        .IsUnique()
                        .HasFilter("[MailId] IS NOT NULL");

                    b.ToTable("MailingGroups");
                });

            modelBuilder.Entity("Mailings.Resources.Domen.Models.UserMails", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("UserMails");
                });

            modelBuilder.Entity("Mailings.Resources.Domen.Models.HtmlMail", b =>
                {
                    b.HasBaseType("Mailings.Resources.Domen.Models.Mail");

                    b.Property<byte[]>("ByteContent")
                        .HasColumnType("varbinary(max)");

                    b.HasDiscriminator().HasValue("HtmlMail");
                });

            modelBuilder.Entity("Mailings.Resources.Domen.Models.TextMail", b =>
                {
                    b.HasBaseType("Mailings.Resources.Domen.Models.Mail");

                    b.Property<string>("StringContent")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("TextMail");
                });

            modelBuilder.Entity("Mailings.Resources.Domen.MailingService.HistoryNoteMailingGroup", b =>
                {
                    b.HasOne("Mailings.Resources.Domen.Models.MailingGroup", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Mailings.Resources.Domen.Models.Attachment", b =>
                {
                    b.HasOne("Mailings.Resources.Domen.Models.Mail", "Mail")
                        .WithMany("Attachments")
                        .HasForeignKey("MailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mail");
                });

            modelBuilder.Entity("Mailings.Resources.Domen.Models.EmailAddressFrom", b =>
                {
                    b.HasOne("Mailings.Resources.Domen.Models.EmailAddress", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mailings.Resources.Domen.Models.MailingGroup", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Mailings.Resources.Domen.Models.EmailAddressTo", b =>
                {
                    b.HasOne("Mailings.Resources.Domen.Models.EmailAddress", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mailings.Resources.Domen.Models.MailingGroup", "Group")
                        .WithMany("To")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Mailings.Resources.Domen.Models.MailingGroup", b =>
                {
                    b.HasOne("Mailings.Resources.Domen.Models.EmailAddressFrom", "From")
                        .WithOne()
                        .HasForeignKey("Mailings.Resources.Domen.Models.MailingGroup", "Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Mailings.Resources.Domen.Models.Mail", "Mail")
                        .WithOne()
                        .HasForeignKey("Mailings.Resources.Domen.Models.MailingGroup", "MailId");

                    b.Navigation("From");

                    b.Navigation("Mail");
                });

            modelBuilder.Entity("Mailings.Resources.Domen.Models.UserMails", b =>
                {
                    b.HasOne("Mailings.Resources.Domen.Models.EmailAddress", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mailings.Resources.Domen.Models.EmailAddress", null)
                        .WithOne()
                        .HasForeignKey("Mailings.Resources.Domen.Models.UserMails", "Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Mailings.Resources.Domen.Models.Mail", b =>
                {
                    b.Navigation("Attachments");
                });

            modelBuilder.Entity("Mailings.Resources.Domen.Models.MailingGroup", b =>
                {
                    b.Navigation("To");
                });
#pragma warning restore 612, 618
        }
    }
}