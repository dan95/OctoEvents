using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OctoEvents.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialScript : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GravatarId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FollowersUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FollowingUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GistsUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StarredUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubscriptionsUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganizationsUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RepositoriesUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventsUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiteAdmin = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(400)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "varchar(400)", nullable: true),
                    ExternalId = table.Column<long>(type: "bigint", nullable: false),
                    NodeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "varchar(2000)", nullable: false),
                    ExternalCreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExternalLastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Repositories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(400)", nullable: false),
                    FullName = table.Column<string>(type: "varchar(400)", nullable: false),
                    Private = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HtmlUrl = table.Column<string>(type: "varchar(400)", nullable: false),
                    Description = table.Column<string>(type: "varchar(400)", nullable: true),
                    Fork = table.Column<bool>(type: "bit", nullable: false),
                    Archived = table.Column<bool>(type: "bit", nullable: false),
                    Disabled = table.Column<bool>(type: "bit", nullable: false),
                    ForkCount = table.Column<long>(type: "bigint", nullable: false),
                    ForksUrl = table.Column<string>(type: "varchar(400)", nullable: true),
                    CollaboratorsUrl = table.Column<string>(type: "varchar(400)", nullable: true),
                    EventsUrl = table.Column<string>(type: "varchar(400)", nullable: true),
                    TagsUrl = table.Column<string>(type: "varchar(400)", nullable: true),
                    GitTagsUrl = table.Column<string>(type: "varchar(400)", nullable: true),
                    ContributorsUrl = table.Column<string>(type: "varchar(400)", nullable: true),
                    SubscribersUrl = table.Column<string>(type: "varchar(400)", nullable: true),
                    SubscriptionUrl = table.Column<string>(type: "varchar(400)", nullable: true),
                    CommitsUrl = table.Column<string>(type: "varchar(400)", nullable: true),
                    DownloadsUrl = table.Column<string>(type: "varchar(400)", nullable: true),
                    IssuesUrl = table.Column<string>(type: "varchar(400)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(400)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "varchar(400)", nullable: true),
                    ExternalId = table.Column<long>(type: "bigint", nullable: false),
                    NodeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "varchar(2000)", nullable: false),
                    ExternalCreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExternalLastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repositories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Repositories_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RepositoryUrl = table.Column<string>(type: "varchar(400)", nullable: false),
                    LabelsUrl = table.Column<string>(type: "varchar(400)", nullable: false),
                    CommentsUrl = table.Column<string>(type: "varchar(400)", nullable: false),
                    HtmlUrl = table.Column<string>(type: "varchar(400)", nullable: false),
                    Number = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "varchar(400)", nullable: true),
                    State = table.Column<string>(type: "varchar(400)", nullable: false),
                    Locked = table.Column<bool>(type: "bit", nullable: false),
                    CommentCount = table.Column<long>(type: "bigint", nullable: false),
                    ClosedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Body = table.Column<string>(type: "varchar(max)", nullable: true),
                    TimelineUrl = table.Column<string>(type: "varchar(400)", nullable: false),
                    StateReason = table.Column<string>(type: "varchar(400)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RepositoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(400)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "varchar(400)", nullable: true),
                    ExternalId = table.Column<long>(type: "bigint", nullable: false),
                    NodeId = table.Column<string>(type: "varchar(400)", nullable: false),
                    Url = table.Column<string>(type: "varchar(400)", nullable: false),
                    ExternalCreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExternalLastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Issues_Repositories_RepositoryId",
                        column: x => x.RepositoryId,
                        principalTable: "Repositories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Issues_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IssueEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Action = table.Column<string>(type: "varchar(400)", nullable: false),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IssueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(400)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "varchar(400)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueEvents_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssueEvents_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_IssueEvents_IssueId",
                table: "IssueEvents",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueEvents_SenderId",
                table: "IssueEvents",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_RepositoryId",
                table: "Issues",
                column: "RepositoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_UserId",
                table: "Issues",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Repositories_OwnerId",
                table: "Repositories",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IssueEvents");

            migrationBuilder.DropTable(
                name: "Issues");

            migrationBuilder.DropTable(
                name: "Repositories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
