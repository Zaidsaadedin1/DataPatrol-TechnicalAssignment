using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cores.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Policies",
                columns: table => new
                {
                    PolicyId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PolicyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    PolicyType = table.Column<int>(type: "int", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policies", x => x.PolicyId);
                });

            migrationBuilder.CreateTable(
                name: "UserGroups",
                columns: table => new
                {
                    GroupId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroups", x => x.GroupId);
                });

            migrationBuilder.CreateTable(
                name: "UserInfos",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfos", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "PolicyTableUserGroup",
                columns: table => new
                {
                    GroupsGroupId = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    PoliciesPolicyId = table.Column<string>(type: "nvarchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyTableUserGroup", x => new { x.GroupsGroupId, x.PoliciesPolicyId });
                    table.ForeignKey(
                        name: "FK_PolicyTableUserGroup_Policies_PoliciesPolicyId",
                        column: x => x.PoliciesPolicyId,
                        principalTable: "Policies",
                        principalColumn: "PolicyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PolicyTableUserGroup_UserGroups_GroupsGroupId",
                        column: x => x.GroupsGroupId,
                        principalTable: "UserGroups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGroupUserInfo",
                columns: table => new
                {
                    GroupsGroupId = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    UsersUserId = table.Column<string>(type: "nvarchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroupUserInfo", x => new { x.GroupsGroupId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_UserGroupUserInfo_UserGroups_GroupsGroupId",
                        column: x => x.GroupsGroupId,
                        principalTable: "UserGroups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroupUserInfo_UserInfos_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "UserInfos",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRequests",
                columns: table => new
                {
                    RequestId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestedBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RequestDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestCode = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CompletionDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RequestedByUserUserId = table.Column<string>(type: "nvarchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRequests", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_UserRequests_UserInfos_RequestedByUserUserId",
                        column: x => x.RequestedByUserUserId,
                        principalTable: "UserInfos",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PolicyTableUserGroup_PoliciesPolicyId",
                table: "PolicyTableUserGroup",
                column: "PoliciesPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupUserInfo_UsersUserId",
                table: "UserGroupUserInfo",
                column: "UsersUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRequests_RequestedByUserUserId",
                table: "UserRequests",
                column: "RequestedByUserUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PolicyTableUserGroup");

            migrationBuilder.DropTable(
                name: "UserGroupUserInfo");

            migrationBuilder.DropTable(
                name: "UserRequests");

            migrationBuilder.DropTable(
                name: "Policies");

            migrationBuilder.DropTable(
                name: "UserGroups");

            migrationBuilder.DropTable(
                name: "UserInfos");
        }
    }
}
