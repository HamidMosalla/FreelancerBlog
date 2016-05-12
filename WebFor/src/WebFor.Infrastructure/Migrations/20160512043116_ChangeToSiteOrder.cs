using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace WebFor.Infrastructure.Migrations
{
    public partial class ChangeToSiteOrder : Migration
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
            migrationBuilder.DropColumn(name: "SiteOrderDoesHaveMultipleCriteriaSearch", schema: "MainDb", table: "SiteOrder");
            migrationBuilder.DropColumn(name: "SiteOrderDoesHaveSearchAjax", schema: "MainDb", table: "SiteOrder");
            migrationBuilder.DropColumn(name: "SiteOrderDoesSuppoerIncluded", schema: "MainDb", table: "SiteOrder");
            migrationBuilder.DropColumn(name: "SiteOrderDoesUseAjaxHeavy", schema: "MainDb", table: "SiteOrder");
            migrationBuilder.DropColumn(name: "SiteOrderDoesUseAjaxLight", schema: "MainDb", table: "SiteOrder");
            migrationBuilder.DropColumn(name: "SiteOrderDoesUseAjaxMedium", schema: "MainDb", table: "SiteOrder");
            migrationBuilder.DropColumn(name: "SiteOrderIsDynamic", schema: "MainDb", table: "SiteOrder");
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderIsSinglePage",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderIsResponsive",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderIsOptimizedForLightness",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderIsCrossPlatform",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderIsAsync",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesSupportTagging",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesSupportCategory",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesSourceCodeIncluded",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesIncludeUserArea",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesIncludeUnitTest",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesIncludeDynamicChart",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesIncludeDomainAndHosting",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesIncludeChart",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveUserProfile",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveSsl",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveSlideShow",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveSiteMap",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveShoppingCartWithoutRegister",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveShoppingCart",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveSeo",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveSearch",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveRegistration",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveNewsLetter",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveFileManager",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveFeed",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveExternalAuth",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveDocumentation",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveDatabase",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveComplexFooter",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveCommenting",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveAdvancedHtmlEditor",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveAdminSection",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesAdminManageUsers",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false);
            migrationBuilder.AddColumn<string>(
                name: "SiteOrderDevelopmentComplexity",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AddColumn<bool>(
                name: "SiteOrderDoesConformToSolidDesign",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddColumn<bool>(
                name: "SiteOrderDoesContentOnUs",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddColumn<bool>(
                name: "SiteOrderDoesHaveBlog",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddColumn<bool>(
                name: "SiteOrderDoesHaveFaq",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddColumn<bool>(
                name: "SiteOrderDoesHaveForum",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddColumn<bool>(
                name: "SiteOrderDoesHaveImageGallery",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddColumn<bool>(
                name: "SiteOrderDoesHaveRatinging",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddColumn<bool>(
                name: "SiteOrderDoesIncludeAdvancedReport",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddColumn<bool>(
                name: "SiteOrderDoesSupportIncluded",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddColumn<bool>(
                name: "SiteOrderDoesUseAjax",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddColumn<string>(
                name: "SiteOrderDoesUseAjaxType",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AddColumn<bool>(
                name: "SiteOrderDoesUseCustomDesign",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddColumn<bool>(
                name: "SiteOrderDoesUseGoogleAnalytics",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddColumn<bool>(
                name: "SiteOrderDoesUseGoogleMap",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddColumn<bool>(
                name: "SiteOrderDoesUseSocialMedia",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddColumn<bool>(
                name: "SiteOrderDoesUseTemplates",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddColumn<bool>(
                name: "SiteOrderDoesVideoPlaying",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddColumn<bool>(
                name: "SiteOrderDoesWithOkWithJavascriptDisabled",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddColumn<bool>(
                name: "SiteOrderIsOptimizedForAccessibility",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddColumn<bool>(
                name: "SiteOrderIsOptimizedForMobile",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddColumn<string>(
                name: "SiteOrderNumberOfMockUp",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "SiteOrderSearchType",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "SiteOrderSeoType",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "SiteOrderSupportType",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "SiteOrderTimeToDeliverMonth",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "SiteOrderWebSiteType",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
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
            migrationBuilder.DropColumn(name: "SiteOrderDevelopmentComplexity", schema: "MainDb", table: "SiteOrder");
            migrationBuilder.DropColumn(name: "SiteOrderDoesConformToSolidDesign", schema: "MainDb", table: "SiteOrder");
            migrationBuilder.DropColumn(name: "SiteOrderDoesContentOnUs", schema: "MainDb", table: "SiteOrder");
            migrationBuilder.DropColumn(name: "SiteOrderDoesHaveBlog", schema: "MainDb", table: "SiteOrder");
            migrationBuilder.DropColumn(name: "SiteOrderDoesHaveFaq", schema: "MainDb", table: "SiteOrder");
            migrationBuilder.DropColumn(name: "SiteOrderDoesHaveForum", schema: "MainDb", table: "SiteOrder");
            migrationBuilder.DropColumn(name: "SiteOrderDoesHaveImageGallery", schema: "MainDb", table: "SiteOrder");
            migrationBuilder.DropColumn(name: "SiteOrderDoesHaveRatinging", schema: "MainDb", table: "SiteOrder");
            migrationBuilder.DropColumn(name: "SiteOrderDoesIncludeAdvancedReport", schema: "MainDb", table: "SiteOrder");
            migrationBuilder.DropColumn(name: "SiteOrderDoesSupportIncluded", schema: "MainDb", table: "SiteOrder");
            migrationBuilder.DropColumn(name: "SiteOrderDoesUseAjax", schema: "MainDb", table: "SiteOrder");
            migrationBuilder.DropColumn(name: "SiteOrderDoesUseAjaxType", schema: "MainDb", table: "SiteOrder");
            migrationBuilder.DropColumn(name: "SiteOrderDoesUseCustomDesign", schema: "MainDb", table: "SiteOrder");
            migrationBuilder.DropColumn(name: "SiteOrderDoesUseGoogleAnalytics", schema: "MainDb", table: "SiteOrder");
            migrationBuilder.DropColumn(name: "SiteOrderDoesUseGoogleMap", schema: "MainDb", table: "SiteOrder");
            migrationBuilder.DropColumn(name: "SiteOrderDoesUseSocialMedia", schema: "MainDb", table: "SiteOrder");
            migrationBuilder.DropColumn(name: "SiteOrderDoesUseTemplates", schema: "MainDb", table: "SiteOrder");
            migrationBuilder.DropColumn(name: "SiteOrderDoesVideoPlaying", schema: "MainDb", table: "SiteOrder");
            migrationBuilder.DropColumn(name: "SiteOrderDoesWithOkWithJavascriptDisabled", schema: "MainDb", table: "SiteOrder");
            migrationBuilder.DropColumn(name: "SiteOrderIsOptimizedForAccessibility", schema: "MainDb", table: "SiteOrder");
            migrationBuilder.DropColumn(name: "SiteOrderIsOptimizedForMobile", schema: "MainDb", table: "SiteOrder");
            migrationBuilder.DropColumn(name: "SiteOrderNumberOfMockUp", schema: "MainDb", table: "SiteOrder");
            migrationBuilder.DropColumn(name: "SiteOrderSearchType", schema: "MainDb", table: "SiteOrder");
            migrationBuilder.DropColumn(name: "SiteOrderSeoType", schema: "MainDb", table: "SiteOrder");
            migrationBuilder.DropColumn(name: "SiteOrderSupportType", schema: "MainDb", table: "SiteOrder");
            migrationBuilder.DropColumn(name: "SiteOrderTimeToDeliverMonth", schema: "MainDb", table: "SiteOrder");
            migrationBuilder.DropColumn(name: "SiteOrderWebSiteType", schema: "MainDb", table: "SiteOrder");
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderIsSinglePage",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderIsResponsive",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderIsOptimizedForLightness",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderIsCrossPlatform",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderIsAsync",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesSupportTagging",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesSupportCategory",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesSourceCodeIncluded",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesIncludeUserArea",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesIncludeUnitTest",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesIncludeDynamicChart",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesIncludeDomainAndHosting",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesIncludeChart",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveUserProfile",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveSsl",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveSlideShow",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveSiteMap",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveShoppingCartWithoutRegister",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveShoppingCart",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveSeo",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveSearch",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveRegistration",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveNewsLetter",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveFileManager",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveFeed",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveExternalAuth",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveDocumentation",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveDatabase",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveComplexFooter",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveCommenting",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveAdvancedHtmlEditor",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesHaveAdminSection",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AlterColumn<bool>(
                name: "SiteOrderDoesAdminManageUsers",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AddColumn<bool>(
                name: "SiteOrderDoesHaveMultipleCriteriaSearch",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AddColumn<bool>(
                name: "SiteOrderDoesHaveSearchAjax",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AddColumn<bool>(
                name: "SiteOrderDoesSuppoerIncluded",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AddColumn<bool>(
                name: "SiteOrderDoesUseAjaxHeavy",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AddColumn<bool>(
                name: "SiteOrderDoesUseAjaxLight",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AddColumn<bool>(
                name: "SiteOrderDoesUseAjaxMedium",
                schema: "MainDb",
                table: "SiteOrder",
                nullable: true);
            migrationBuilder.AddColumn<bool>(
                name: "SiteOrderIsDynamic",
                schema: "MainDb",
                table: "SiteOrder",
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
