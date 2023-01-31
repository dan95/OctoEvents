﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OctoEvents.Infrastructure.Data;

#nullable disable

namespace OctoEvents.Infrastructure.Data.Migrations
{
    [DbContext(typeof(OctoEventsDbContext))]
    [Migration("20230129213445_InitialScript")]
    partial class InitialScript
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OctoEvents.Domain.Entities.Issue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Body")
                        .HasColumnType("varchar(max)")
                        .HasColumnName("Body");

                    b.Property<DateTime?>("ClosedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("ClosedAt");

                    b.Property<long>("CommentCount")
                        .HasColumnType("bigint")
                        .HasColumnName("CommentCount");

                    b.Property<string>("CommentsUrl")
                        .IsRequired()
                        .HasColumnType("varchar(400)")
                        .HasColumnName("CommentsUrl");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedAt");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(400)")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime?>("ExternalCreationDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("ExternalCreationDate");

                    b.Property<long>("ExternalId")
                        .HasColumnType("bigint")
                        .HasColumnName("ExternalId");

                    b.Property<DateTime?>("ExternalLastUpdate")
                        .HasColumnType("datetime2")
                        .HasColumnName("ExternalLastUpdate");

                    b.Property<string>("HtmlUrl")
                        .IsRequired()
                        .HasColumnType("varchar(400)")
                        .HasColumnName("HtmlUrl");

                    b.Property<string>("LabelsUrl")
                        .IsRequired()
                        .HasColumnType("varchar(400)")
                        .HasColumnName("LabelsUrl");

                    b.Property<bool>("Locked")
                        .HasColumnType("bit")
                        .HasColumnName("Locked");

                    b.Property<string>("NodeId")
                        .IsRequired()
                        .HasColumnType("varchar(400)")
                        .HasColumnName("NodeId");

                    b.Property<long>("Number")
                        .HasColumnType("bigint")
                        .HasColumnName("Number");

                    b.Property<Guid>("RepositoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RepositoryUrl")
                        .IsRequired()
                        .HasColumnType("varchar(400)")
                        .HasColumnName("RepositoryUrl");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("varchar(400)")
                        .HasColumnName("State");

                    b.Property<string>("StateReason")
                        .HasColumnType("varchar(400)")
                        .HasColumnName("StateReason");

                    b.Property<string>("TimelineUrl")
                        .IsRequired()
                        .HasColumnType("varchar(400)")
                        .HasColumnName("TimelineUrl");

                    b.Property<string>("Title")
                        .HasColumnType("varchar(400)")
                        .HasColumnName("Title");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdatedAt");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("varchar(400)")
                        .HasColumnName("UpdatedBy");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("varchar(400)")
                        .HasColumnName("Url");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RepositoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Issues");
                });

            modelBuilder.Entity("OctoEvents.Domain.Entities.IssueEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnType("varchar(400)")
                        .HasColumnName("Action");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedAt");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(400)")
                        .HasColumnName("CreatedBy");

                    b.Property<Guid>("IssueId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdatedAt");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("varchar(400)")
                        .HasColumnName("UpdatedBy");

                    b.HasKey("Id");

                    b.HasIndex("IssueId");

                    b.HasIndex("SenderId");

                    b.ToTable("IssueEvents");
                });

            modelBuilder.Entity("OctoEvents.Domain.Entities.Repository", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Archived")
                        .HasColumnType("bit")
                        .HasColumnName("Archived");

                    b.Property<string>("CollaboratorsUrl")
                        .HasColumnType("varchar(400)")
                        .HasColumnName("CollaboratorsUrl");

                    b.Property<string>("CommitsUrl")
                        .HasColumnType("varchar(400)")
                        .HasColumnName("CommitsUrl");

                    b.Property<string>("ContributorsUrl")
                        .HasColumnType("varchar(400)")
                        .HasColumnName("ContributorsUrl");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedAt");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(400)")
                        .HasColumnName("CreatedBy");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(400)")
                        .HasColumnName("Description");

                    b.Property<bool>("Disabled")
                        .HasColumnType("bit")
                        .HasColumnName("Disabled");

                    b.Property<string>("DownloadsUrl")
                        .HasColumnType("varchar(400)")
                        .HasColumnName("DownloadsUrl");

                    b.Property<string>("EventsUrl")
                        .HasColumnType("varchar(400)")
                        .HasColumnName("EventsUrl");

                    b.Property<DateTime?>("ExternalCreationDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("ExternalCreationDate");

                    b.Property<long>("ExternalId")
                        .HasColumnType("bigint")
                        .HasColumnName("ExternalId");

                    b.Property<DateTime?>("ExternalLastUpdate")
                        .HasColumnType("datetime2")
                        .HasColumnName("ExternalLastUpdate");

                    b.Property<bool>("Fork")
                        .HasColumnType("bit")
                        .HasColumnName("Fork");

                    b.Property<long>("ForkCount")
                        .HasColumnType("bigint")
                        .HasColumnName("ForkCount");

                    b.Property<string>("ForksUrl")
                        .HasColumnType("varchar(400)")
                        .HasColumnName("ForksUrl");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("varchar(400)")
                        .HasColumnName("FullName");

                    b.Property<string>("GitTagsUrl")
                        .HasColumnType("varchar(400)")
                        .HasColumnName("GitTagsUrl");

                    b.Property<string>("HtmlUrl")
                        .IsRequired()
                        .HasColumnType("varchar(400)")
                        .HasColumnName("HtmlUrl");

                    b.Property<string>("IssuesUrl")
                        .HasColumnType("varchar(400)")
                        .HasColumnName("IssuesUrl");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(400)")
                        .HasColumnName("Name");

                    b.Property<string>("NodeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Private")
                        .HasColumnType("bit")
                        .HasColumnName("Private");

                    b.Property<string>("SubscribersUrl")
                        .HasColumnType("varchar(400)")
                        .HasColumnName("SubscribersUrl");

                    b.Property<string>("SubscriptionUrl")
                        .HasColumnType("varchar(400)")
                        .HasColumnName("SubscriptionUrl");

                    b.Property<string>("TagsUrl")
                        .HasColumnType("varchar(400)")
                        .HasColumnName("TagsUrl");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdatedAt");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("varchar(400)")
                        .HasColumnName("UpdatedBy");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("varchar(2000)")
                        .HasColumnName("Url");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Repositories");
                });

            modelBuilder.Entity("OctoEvents.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AvatarUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedAt");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(400)")
                        .HasColumnName("CreatedBy");

                    b.Property<string>("EventsUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ExternalCreationDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("ExternalCreationDate");

                    b.Property<long>("ExternalId")
                        .HasColumnType("bigint")
                        .HasColumnName("ExternalId");

                    b.Property<DateTime?>("ExternalLastUpdate")
                        .HasColumnType("datetime2")
                        .HasColumnName("ExternalLastUpdate");

                    b.Property<string>("FollowersUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FollowingUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GistsUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GravatarId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NodeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrganizationsUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RepositoriesUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SiteAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("StarredUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubscriptionsUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdatedAt");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("varchar(400)")
                        .HasColumnName("UpdatedBy");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("varchar(2000)")
                        .HasColumnName("Url");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OctoEvents.Domain.Entities.Issue", b =>
                {
                    b.HasOne("OctoEvents.Domain.Entities.Repository", "Repository")
                        .WithMany("Issues")
                        .HasForeignKey("RepositoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OctoEvents.Domain.Entities.User", "User")
                        .WithMany("Issues")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Repository");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OctoEvents.Domain.Entities.IssueEvent", b =>
                {
                    b.HasOne("OctoEvents.Domain.Entities.Issue", "Issue")
                        .WithMany("Events")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OctoEvents.Domain.Entities.User", "Sender")
                        .WithMany("Events")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Issue");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("OctoEvents.Domain.Entities.Repository", b =>
                {
                    b.HasOne("OctoEvents.Domain.Entities.User", "Owner")
                        .WithMany("Repositories")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("OctoEvents.Domain.Entities.Issue", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("OctoEvents.Domain.Entities.Repository", b =>
                {
                    b.Navigation("Issues");
                });

            modelBuilder.Entity("OctoEvents.Domain.Entities.User", b =>
                {
                    b.Navigation("Events");

                    b.Navigation("Issues");

                    b.Navigation("Repositories");
                });
#pragma warning restore 612, 618
        }
    }
}
