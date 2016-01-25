using System;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;

namespace WebFor.Infrastructure.EntityFramework.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema("MainDb");
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "MainDb",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "MainDb",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserAddress = table.Column<string>(nullable: true),
                    UserAvatar = table.Column<string>(nullable: true),
                    UserBanEndDate = table.Column<DateTime>(nullable: true),
                    UserBio = table.Column<string>(nullable: true),
                    UserDateOfBirth = table.Column<DateTime>(nullable: true),
                    UserFacebookProfile = table.Column<string>(nullable: true),
                    UserFavourites = table.Column<string>(nullable: true),
                    UserFullName = table.Column<string>(nullable: false),
                    UserGender = table.Column<string>(nullable: false),
                    UserGoogleProfile = table.Column<string>(nullable: true),
                    UserHowFindUs = table.Column<string>(nullable: true),
                    UserLinkedInProfile = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    UserOccupation = table.Column<string>(nullable: true),
                    UserPoints = table.Column<int>(nullable: true),
                    UserRegisteredDate = table.Column<DateTime>(nullable: false),
                    UserSpeciality = table.Column<string>(nullable: true),
                    UserTwitterProfile = table.Column<string>(nullable: true),
                    UserWebSite = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "ArticleTag",
                schema: "MainDb",
                columns: table => new
                {
                    ArticleTagId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArticleTagName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleTag", x => x.ArticleTagId);
                });
            migrationBuilder.CreateTable(
                name: "Contact",
                schema: "MainDb",
                columns: table => new
                {
                    ContactId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContactBody = table.Column<string>(nullable: false),
                    ContactDate = table.Column<DateTime>(nullable: false),
                    ContactEmail = table.Column<string>(nullable: false),
                    ContactName = table.Column<string>(nullable: false),
                    ContactPhone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.ContactId);
                });
            migrationBuilder.CreateTable(
                name: "Portfolio",
                schema: "MainDb",
                columns: table => new
                {
                    PortfolioId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PortfolioBody = table.Column<string>(nullable: false),
                    PortfolioCategory = table.Column<string>(nullable: true),
                    PortfolioDateBuilt = table.Column<DateTime>(nullable: false),
                    PortfolioDateCreated = table.Column<DateTime>(nullable: false),
                    PortfolioSiteAddress = table.Column<string>(nullable: true),
                    PortfolioThumbnail = table.Column<string>(nullable: false),
                    PortfolioTitle = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolio", x => x.PortfolioId);
                });
            migrationBuilder.CreateTable(
                name: "SiteOrder",
                schema: "MainDb",
                columns: table => new
                {
                    SiteOrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SiteOrderDesc = table.Column<string>(nullable: false),
                    SiteOrderDoesAdminManageUsers = table.Column<bool>(nullable: true),
                    SiteOrderDoesHaveAdminSection = table.Column<bool>(nullable: true),
                    SiteOrderDoesHaveAdvancedHtmlEditor = table.Column<bool>(nullable: true),
                    SiteOrderDoesHaveCommenting = table.Column<bool>(nullable: true),
                    SiteOrderDoesHaveComplexFooter = table.Column<bool>(nullable: true),
                    SiteOrderDoesHaveDatabase = table.Column<bool>(nullable: true),
                    SiteOrderDoesHaveDocumentation = table.Column<bool>(nullable: true),
                    SiteOrderDoesHaveExternalAuth = table.Column<bool>(nullable: true),
                    SiteOrderDoesHaveFeed = table.Column<bool>(nullable: true),
                    SiteOrderDoesHaveFileManager = table.Column<bool>(nullable: true),
                    SiteOrderDoesHaveMultipleCriteriaSearch = table.Column<bool>(nullable: true),
                    SiteOrderDoesHaveNewsLetter = table.Column<bool>(nullable: true),
                    SiteOrderDoesHaveRegistration = table.Column<bool>(nullable: true),
                    SiteOrderDoesHaveSearch = table.Column<bool>(nullable: true),
                    SiteOrderDoesHaveSearchAjax = table.Column<bool>(nullable: true),
                    SiteOrderDoesHaveSeo = table.Column<bool>(nullable: true),
                    SiteOrderDoesHaveShoppingCart = table.Column<bool>(nullable: true),
                    SiteOrderDoesHaveShoppingCartWithoutRegister = table.Column<bool>(nullable: true),
                    SiteOrderDoesHaveSiteMap = table.Column<bool>(nullable: true),
                    SiteOrderDoesHaveSlideShow = table.Column<bool>(nullable: true),
                    SiteOrderDoesHaveSsl = table.Column<bool>(nullable: true),
                    SiteOrderDoesHaveUserProfile = table.Column<bool>(nullable: true),
                    SiteOrderDoesIncludeChart = table.Column<bool>(nullable: true),
                    SiteOrderDoesIncludeDomainAndHosting = table.Column<bool>(nullable: true),
                    SiteOrderDoesIncludeDynamicChart = table.Column<bool>(nullable: true),
                    SiteOrderDoesIncludeUnitTest = table.Column<bool>(nullable: true),
                    SiteOrderDoesIncludeUserArea = table.Column<bool>(nullable: true),
                    SiteOrderDoesSourceCodeIncluded = table.Column<bool>(nullable: true),
                    SiteOrderDoesSuppoerIncluded = table.Column<bool>(nullable: true),
                    SiteOrderDoesSupportCategory = table.Column<bool>(nullable: true),
                    SiteOrderDoesSupportTagging = table.Column<bool>(nullable: true),
                    SiteOrderDoesUseAjaxHeavy = table.Column<bool>(nullable: true),
                    SiteOrderDoesUseAjaxLight = table.Column<bool>(nullable: true),
                    SiteOrderDoesUseAjaxMedium = table.Column<bool>(nullable: true),
                    SiteOrderEmail = table.Column<string>(nullable: false),
                    SiteOrderExample = table.Column<string>(nullable: true),
                    SiteOrderFinalPrice = table.Column<decimal>(nullable: true),
                    SiteOrderFullName = table.Column<string>(nullable: false),
                    SiteOrderHowFindUs = table.Column<string>(nullable: true),
                    SiteOrderIsAsync = table.Column<bool>(nullable: true),
                    SiteOrderIsCrossPlatform = table.Column<bool>(nullable: true),
                    SiteOrderIsDynamic = table.Column<bool>(nullable: true),
                    SiteOrderIsOptimizedForLightness = table.Column<bool>(nullable: true),
                    SiteOrderIsResponsive = table.Column<bool>(nullable: true),
                    SiteOrderIsSinglePage = table.Column<bool>(nullable: true),
                    SiteOrderPhone = table.Column<string>(nullable: false),
                    SiteOrderStaticPageNumber = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteOrder", x => x.SiteOrderId);
                });
            migrationBuilder.CreateTable(
                name: "SlideShow",
                schema: "MainDb",
                columns: table => new
                {
                    SlideShowId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SlideShowBody = table.Column<string>(nullable: false),
                    SlideShowDateCreated = table.Column<DateTime>(nullable: false),
                    SlideShowLink = table.Column<string>(nullable: true),
                    SlideShowPictrure = table.Column<string>(nullable: false),
                    SlideShowPriority = table.Column<int>(nullable: true),
                    SlideShowTitle = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlideShow", x => x.SlideShowId);
                });
            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "MainDb",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRoleClaim<string>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "MainDb",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "MainDb",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserClaim<string>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "MainDb",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "MainDb",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserLogin<string>", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "MainDb",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "MainDb",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserRole<string>", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_IdentityUserRole<string>_IdentityRole_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "MainDb",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IdentityUserRole<string>_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "MainDb",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Article",
                schema: "MainDb",
                columns: table => new
                {
                    ArticleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArticleBody = table.Column<string>(nullable: false),
                    ArticleDateCreated = table.Column<DateTime>(nullable: false),
                    ArticleDateModified = table.Column<DateTime>(nullable: true),
                    ArticleFileUrl = table.Column<string>(nullable: true),
                    ArticleStatus = table.Column<string>(nullable: false),
                    ArticleSummary = table.Column<string>(nullable: false),
                    ArticleThumbnailUrl = table.Column<string>(nullable: true),
                    ArticleTitle = table.Column<string>(nullable: false),
                    ArticleVideoUrl = table.Column<string>(nullable: true),
                    ArticleViewCount = table.Column<long>(nullable: true),
                    UserIDfk = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.ArticleId);
                    table.ForeignKey(
                        name: "FK_Article_ApplicationUser_UserIDfk",
                        column: x => x.UserIDfk,
                        principalSchema: "MainDb",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "ArticleArticleTag",
                schema: "MainDb",
                columns: table => new
                {
                    ArticleId = table.Column<int>(nullable: false),
                    ArticleTagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleArticleTag", x => new { x.ArticleId, x.ArticleTagId });
                    table.ForeignKey(
                        name: "FK_ArticleArticleTag_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalSchema: "MainDb",
                        principalTable: "Article",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleArticleTag_ArticleTag_ArticleTagId",
                        column: x => x.ArticleTagId,
                        principalSchema: "MainDb",
                        principalTable: "ArticleTag",
                        principalColumn: "ArticleTagId",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "ArticleComment",
                schema: "MainDb",
                columns: table => new
                {
                    ArticleCommentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArticleCommentBody = table.Column<string>(nullable: false),
                    ArticleCommentDateCreated = table.Column<DateTime>(nullable: false),
                    ArticleCommentEmail = table.Column<string>(nullable: false),
                    ArticleCommentName = table.Column<string>(nullable: false),
                    ArticleCommentParentId = table.Column<int>(nullable: true),
                    ArticleCommentWebSite = table.Column<string>(nullable: true),
                    ArticleIDfk = table.Column<int>(nullable: false),
                    UserIDfk = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleComment", x => x.ArticleCommentId);
                    table.ForeignKey(
                        name: "FK_ArticleComment_ArticleComment_ArticleCommentParentId",
                        column: x => x.ArticleCommentParentId,
                        principalSchema: "MainDb",
                        principalTable: "ArticleComment",
                        principalColumn: "ArticleCommentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ArticleComment_Article_ArticleIDfk",
                        column: x => x.ArticleIDfk,
                        principalSchema: "MainDb",
                        principalTable: "Article",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleComment_ApplicationUser_UserIDfk",
                        column: x => x.UserIDfk,
                        principalSchema: "MainDb",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "ArticleRating",
                schema: "MainDb",
                columns: table => new
                {
                    ArticleRatingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArticleIDfk = table.Column<int>(nullable: false),
                    ArticleRatingScore = table.Column<int>(nullable: false),
                    UserIDfk = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleRating", x => x.ArticleRatingId);
                    table.ForeignKey(
                        name: "FK_ArticleRating_Article_ArticleIDfk",
                        column: x => x.ArticleIDfk,
                        principalSchema: "MainDb",
                        principalTable: "Article",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleRating_ApplicationUser_UserIDfk",
                        column: x => x.UserIDfk,
                        principalSchema: "MainDb",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "MainDb",
                table: "AspNetRoles",
                column: "NormalizedName");
            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "MainDb",
                table: "AspNetUsers",
                column: "NormalizedEmail");
            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "MainDb",
                table: "AspNetUsers",
                column: "NormalizedUserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "AspNetRoleClaims", schema: "MainDb");
            migrationBuilder.DropTable(name: "AspNetUserClaims", schema: "MainDb");
            migrationBuilder.DropTable(name: "AspNetUserLogins", schema: "MainDb");
            migrationBuilder.DropTable(name: "AspNetUserRoles", schema: "MainDb");
            migrationBuilder.DropTable(name: "ArticleArticleTag", schema: "MainDb");
            migrationBuilder.DropTable(name: "ArticleComment", schema: "MainDb");
            migrationBuilder.DropTable(name: "ArticleRating", schema: "MainDb");
            migrationBuilder.DropTable(name: "Contact", schema: "MainDb");
            migrationBuilder.DropTable(name: "Portfolio", schema: "MainDb");
            migrationBuilder.DropTable(name: "SiteOrder", schema: "MainDb");
            migrationBuilder.DropTable(name: "SlideShow", schema: "MainDb");
            migrationBuilder.DropTable(name: "AspNetRoles", schema: "MainDb");
            migrationBuilder.DropTable(name: "ArticleTag", schema: "MainDb");
            migrationBuilder.DropTable(name: "Article", schema: "MainDb");
            migrationBuilder.DropTable(name: "AspNetUsers", schema: "MainDb");
        }
    }
}
