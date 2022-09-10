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
    [Migration("20220910054300_CascadeMailDeleting5")]
    partial class CascadeMailDeleting5
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Mailings.Resources.Domain.MainModels.Attachment", b =>
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

            modelBuilder.Entity("Mailings.Resources.Domain.MainModels.EmailAddress", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AddressString")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EmailAddress");
                });

            modelBuilder.Entity("Mailings.Resources.Domain.MainModels.EmailAddressFrom", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PseudoName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EmailSenders", (string)null);
                });

            modelBuilder.Entity("Mailings.Resources.Domain.MainModels.EmailAddressTo", b =>
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

            modelBuilder.Entity("Mailings.Resources.Domain.MainModels.HistoryNoteMailingGroup", b =>
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

            modelBuilder.Entity("Mailings.Resources.Domain.MainModels.Mail", b =>
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

            modelBuilder.Entity("Mailings.Resources.Domain.MainModels.MailingGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MailId");

                    b.ToTable("MailingGroups");
                });

            modelBuilder.Entity("Mailings.Resources.Domain.MainModels.HtmlMail", b =>
                {
                    b.HasBaseType("Mailings.Resources.Domain.MainModels.Mail");

                    b.Property<byte[]>("ByteContent")
                        .HasColumnType("varbinary(max)");

                    b.HasDiscriminator().HasValue("HtmlMail");
                });

            modelBuilder.Entity("Mailings.Resources.Domain.MainModels.TextMail", b =>
                {
                    b.HasBaseType("Mailings.Resources.Domain.MainModels.Mail");

                    b.Property<string>("StringContent")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("TextMail");
                });

            modelBuilder.Entity("Mailings.Resources.Domain.MainModels.Attachment", b =>
                {
                    b.HasOne("Mailings.Resources.Domain.MainModels.Mail", "Mail")
                        .WithMany("Attachments")
                        .HasForeignKey("MailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mail");
                });

            modelBuilder.Entity("Mailings.Resources.Domain.MainModels.EmailAddressTo", b =>
                {
                    b.HasOne("Mailings.Resources.Domain.MainModels.EmailAddress", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mailings.Resources.Domain.MainModels.MailingGroup", "Group")
                        .WithMany("To")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Mailings.Resources.Domain.MainModels.HistoryNoteMailingGroup", b =>
                {
                    b.HasOne("Mailings.Resources.Domain.MainModels.MailingGroup", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Mailings.Resources.Domain.MainModels.MailingGroup", b =>
                {
                    b.HasOne("Mailings.Resources.Domain.MainModels.EmailAddressFrom", "From")
                        .WithOne("Group")
                        .HasForeignKey("Mailings.Resources.Domain.MainModels.MailingGroup", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mailings.Resources.Domain.MainModels.Mail", "Mail")
                        .WithMany()
                        .HasForeignKey("MailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("From");

                    b.Navigation("Mail");
                });

            modelBuilder.Entity("Mailings.Resources.Domain.MainModels.EmailAddressFrom", b =>
                {
                    b.Navigation("Group")
                        .IsRequired();
                });

            modelBuilder.Entity("Mailings.Resources.Domain.MainModels.Mail", b =>
                {
                    b.Navigation("Attachments");
                });

            modelBuilder.Entity("Mailings.Resources.Domain.MainModels.MailingGroup", b =>
                {
                    b.Navigation("To");
                });
#pragma warning restore 612, 618
        }
    }
}
