using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace WebFor.Migrations
{
    public partial class ChangeOnArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", schema: "MainDb", table: "AspNetRoleClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId", schema: "MainDb", table: "AspNetUserClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId", schema: "MainDb", table: "AspNetUserLogins");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityRole_RoleId", schema: "MainDb", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_ApplicationUser_UserId", schema: "MainDb", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_ArticleArticleTag_Article_ArticleId", schema: "MainDb", table: "ArticleArticleTag");
            migrationBuilder.DropForeignKey(name: "FK_ArticleArticleTag_ArticleTag_ArticleTagId", schema: "MainDb", table: "ArticleArticleTag");
            migrationBuilder.DropForeignKey(name: "FK_ArticleComment_Article_ArticleIDfk", schema: "MainDb", table: "ArticleComment");
            migrationBuilder.DropForeignKey(name: "FK_ArticleRating_Article_ArticleIDfk", schema: "MainDb", table: "ArticleRating");
            migrationBuilder.DropColumn(name: "ArticleFileUrl", schema: "MainDb", table: "Article");
            migrationBuilder.DropColumn(name: "ArticleThumbnailUrl", schema: "MainDb", table: "Article");
            migrationBuilder.DropColumn(name: "ArticleVideoUrl", schema: "MainDb", table: "Article");
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId",
                schema: "MainDb",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalSchema: "MainDb",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId",
                schema: "MainDb",
                table: "AspNetUserClaims",
                column: "UserId",
                principalSchema: "MainDb",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId",
                schema: "MainDb",
                table: "AspNetUserLogins",
                column: "UserId",
                principalSchema: "MainDb",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_IdentityRole_RoleId",
                schema: "MainDb",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalSchema: "MainDb",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_ApplicationUser_UserId",
                schema: "MainDb",
                table: "AspNetUserRoles",
                column: "UserId",
                principalSchema: "MainDb",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_ArticleArticleTag_Article_ArticleId",
                schema: "MainDb",
                table: "ArticleArticleTag",
                column: "ArticleId",
                principalSchema: "MainDb",
                principalTable: "Article",
                principalColumn: "ArticleId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_ArticleArticleTag_ArticleTag_ArticleTagId",
                schema: "MainDb",
                table: "ArticleArticleTag",
                column: "ArticleTagId",
                principalSchema: "MainDb",
                principalTable: "ArticleTag",
                principalColumn: "ArticleTagId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_ArticleComment_Article_ArticleIDfk",
                schema: "MainDb",
                table: "ArticleComment",
                column: "ArticleIDfk",
                principalSchema: "MainDb",
                principalTable: "Article",
                principalColumn: "ArticleId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_ArticleRating_Article_ArticleIDfk",
                schema: "MainDb",
                table: "ArticleRating",
                column: "ArticleIDfk",
                principalSchema: "MainDb",
                principalTable: "Article",
                principalColumn: "ArticleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", schema: "MainDb", table: "AspNetRoleClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId", schema: "MainDb", table: "AspNetUserClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId", schema: "MainDb", table: "AspNetUserLogins");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityRole_RoleId", schema: "MainDb", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_ApplicationUser_UserId", schema: "MainDb", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_ArticleArticleTag_Article_ArticleId", schema: "MainDb", table: "ArticleArticleTag");
            migrationBuilder.DropForeignKey(name: "FK_ArticleArticleTag_ArticleTag_ArticleTagId", schema: "MainDb", table: "ArticleArticleTag");
            migrationBuilder.DropForeignKey(name: "FK_ArticleComment_Article_ArticleIDfk", schema: "MainDb", table: "ArticleComment");
            migrationBuilder.DropForeignKey(name: "FK_ArticleRating_Article_ArticleIDfk", schema: "MainDb", table: "ArticleRating");
            migrationBuilder.AddColumn<string>(
                name: "ArticleFileUrl",
                schema: "MainDb",
                table: "Article",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "ArticleThumbnailUrl",
                schema: "MainDb",
                table: "Article",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "ArticleVideoUrl",
                schema: "MainDb",
                table: "Article",
                nullable: true);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId",
                schema: "MainDb",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalSchema: "MainDb",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId",
                schema: "MainDb",
                table: "AspNetUserClaims",
                column: "UserId",
                principalSchema: "MainDb",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId",
                schema: "MainDb",
                table: "AspNetUserLogins",
                column: "UserId",
                principalSchema: "MainDb",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_IdentityRole_RoleId",
                schema: "MainDb",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalSchema: "MainDb",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_ApplicationUser_UserId",
                schema: "MainDb",
                table: "AspNetUserRoles",
                column: "UserId",
                principalSchema: "MainDb",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_ArticleArticleTag_Article_ArticleId",
                schema: "MainDb",
                table: "ArticleArticleTag",
                column: "ArticleId",
                principalSchema: "MainDb",
                principalTable: "Article",
                principalColumn: "ArticleId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_ArticleArticleTag_ArticleTag_ArticleTagId",
                schema: "MainDb",
                table: "ArticleArticleTag",
                column: "ArticleTagId",
                principalSchema: "MainDb",
                principalTable: "ArticleTag",
                principalColumn: "ArticleTagId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_ArticleComment_Article_ArticleIDfk",
                schema: "MainDb",
                table: "ArticleComment",
                column: "ArticleIDfk",
                principalSchema: "MainDb",
                principalTable: "Article",
                principalColumn: "ArticleId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_ArticleRating_Article_ArticleIDfk",
                schema: "MainDb",
                table: "ArticleRating",
                column: "ArticleIDfk",
                principalSchema: "MainDb",
                principalTable: "Article",
                principalColumn: "ArticleId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
