using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebFor.Infrastructure.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "MainDb");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "MainDb",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "MainDb",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "MainDb",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
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
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    UserOccupation = table.Column<string>(nullable: true),
                    UserPhoneNumber = table.Column<string>(nullable: true),
                    UserPoints = table.Column<int>(nullable: true),
                    UserProfileEmail = table.Column<string>(nullable: true),
                    UserRegisteredDate = table.Column<DateTime>(nullable: false),
                    UserSpeciality = table.Column<string>(nullable: true),
                    UserTwitterProfile = table.Column<string>(nullable: true),
                    UserWebSite = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleTags",
                schema: "MainDb",
                columns: table => new
                {
                    ArticleTagId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArticleTagName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleTags", x => x.ArticleTagId);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
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
                    table.PrimaryKey("PK_Contacts", x => x.ContactId);
                });

            migrationBuilder.CreateTable(
                name: "Portfolios",
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
                    table.PrimaryKey("PK_Portfolios", x => x.PortfolioId);
                });

            migrationBuilder.CreateTable(
                name: "SiteOrders",
                schema: "MainDb",
                columns: table => new
                {
                    SiteOrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SiteOrderDesc = table.Column<string>(nullable: false),
                    SiteOrderDevelopmentComplexity = table.Column<string>(nullable: true),
                    SiteOrderDoesAdminManageUsers = table.Column<bool>(nullable: false),
                    SiteOrderDoesConformToSolidDesign = table.Column<bool>(nullable: false),
                    SiteOrderDoesContentOnUs = table.Column<bool>(nullable: false),
                    SiteOrderDoesHaveAdminSection = table.Column<bool>(nullable: false),
                    SiteOrderDoesHaveAdvancedHtmlEditor = table.Column<bool>(nullable: false),
                    SiteOrderDoesHaveBlog = table.Column<bool>(nullable: false),
                    SiteOrderDoesHaveCommenting = table.Column<bool>(nullable: false),
                    SiteOrderDoesHaveComplexFooter = table.Column<bool>(nullable: false),
                    SiteOrderDoesHaveDatabase = table.Column<bool>(nullable: false),
                    SiteOrderDoesHaveDocumentation = table.Column<bool>(nullable: false),
                    SiteOrderDoesHaveExternalAuth = table.Column<bool>(nullable: false),
                    SiteOrderDoesHaveFaq = table.Column<bool>(nullable: false),
                    SiteOrderDoesHaveFeed = table.Column<bool>(nullable: false),
                    SiteOrderDoesHaveFileManager = table.Column<bool>(nullable: false),
                    SiteOrderDoesHaveForum = table.Column<bool>(nullable: false),
                    SiteOrderDoesHaveImageGallery = table.Column<bool>(nullable: false),
                    SiteOrderDoesHaveNewsLetter = table.Column<bool>(nullable: false),
                    SiteOrderDoesHaveRatinging = table.Column<bool>(nullable: false),
                    SiteOrderDoesHaveRegistration = table.Column<bool>(nullable: false),
                    SiteOrderDoesHaveSearch = table.Column<bool>(nullable: false),
                    SiteOrderDoesHaveSeo = table.Column<bool>(nullable: false),
                    SiteOrderDoesHaveShoppingCart = table.Column<bool>(nullable: false),
                    SiteOrderDoesHaveShoppingCartWithoutRegister = table.Column<bool>(nullable: false),
                    SiteOrderDoesHaveSiteMap = table.Column<bool>(nullable: false),
                    SiteOrderDoesHaveSlideShow = table.Column<bool>(nullable: false),
                    SiteOrderDoesHaveSsl = table.Column<bool>(nullable: false),
                    SiteOrderDoesHaveUserProfile = table.Column<bool>(nullable: false),
                    SiteOrderDoesIncludeAdvancedReport = table.Column<bool>(nullable: false),
                    SiteOrderDoesIncludeChart = table.Column<bool>(nullable: false),
                    SiteOrderDoesIncludeDomainAndHosting = table.Column<bool>(nullable: false),
                    SiteOrderDoesIncludeDynamicChart = table.Column<bool>(nullable: false),
                    SiteOrderDoesIncludeUnitTest = table.Column<bool>(nullable: false),
                    SiteOrderDoesIncludeUserArea = table.Column<bool>(nullable: false),
                    SiteOrderDoesSourceCodeIncluded = table.Column<bool>(nullable: false),
                    SiteOrderDoesSupportCategory = table.Column<bool>(nullable: false),
                    SiteOrderDoesSupportIncluded = table.Column<bool>(nullable: false),
                    SiteOrderDoesSupportTagging = table.Column<bool>(nullable: false),
                    SiteOrderDoesUseAjax = table.Column<bool>(nullable: false),
                    SiteOrderDoesUseAjaxType = table.Column<string>(nullable: true),
                    SiteOrderDoesUseCustomDesign = table.Column<bool>(nullable: false),
                    SiteOrderDoesUseGoogleAnalytics = table.Column<bool>(nullable: false),
                    SiteOrderDoesUseGoogleMap = table.Column<bool>(nullable: false),
                    SiteOrderDoesUseSocialMedia = table.Column<bool>(nullable: false),
                    SiteOrderDoesUseTemplates = table.Column<bool>(nullable: false),
                    SiteOrderDoesVideoPlaying = table.Column<bool>(nullable: false),
                    SiteOrderDoesWithOkWithJavascriptDisabled = table.Column<bool>(nullable: false),
                    SiteOrderEmail = table.Column<string>(nullable: false),
                    SiteOrderExample = table.Column<string>(nullable: true),
                    SiteOrderFinalPrice = table.Column<decimal>(nullable: true),
                    SiteOrderFullName = table.Column<string>(nullable: false),
                    SiteOrderHowFindUs = table.Column<string>(nullable: true),
                    SiteOrderIsAsync = table.Column<bool>(nullable: false),
                    SiteOrderIsCrossPlatform = table.Column<bool>(nullable: false),
                    SiteOrderIsOptimizedForAccessibility = table.Column<bool>(nullable: false),
                    SiteOrderIsOptimizedForLightness = table.Column<bool>(nullable: false),
                    SiteOrderIsOptimizedForMobile = table.Column<bool>(nullable: false),
                    SiteOrderIsResponsive = table.Column<bool>(nullable: false),
                    SiteOrderIsSinglePage = table.Column<bool>(nullable: false),
                    SiteOrderNumberOfMockUp = table.Column<string>(nullable: true),
                    SiteOrderPhone = table.Column<string>(nullable: false),
                    SiteOrderSearchType = table.Column<string>(nullable: true),
                    SiteOrderSeoType = table.Column<string>(nullable: true),
                    SiteOrderStaticPageNumber = table.Column<int>(nullable: true),
                    SiteOrderSupportType = table.Column<string>(nullable: true),
                    SiteOrderTimeToDeliverMonth = table.Column<string>(nullable: true),
                    SiteOrderWebSiteType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteOrders", x => x.SiteOrderId);
                });

            migrationBuilder.CreateTable(
                name: "SlideShows",
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
                    table.PrimaryKey("PK_SlideShows", x => x.SlideShowId);
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
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
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
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
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
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
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
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "MainDb",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "MainDb",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                schema: "MainDb",
                columns: table => new
                {
                    ArticleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArticleBody = table.Column<string>(nullable: false),
                    ArticleDateCreated = table.Column<DateTime>(nullable: false),
                    ArticleDateModified = table.Column<DateTime>(nullable: true),
                    ArticleStatus = table.Column<string>(nullable: false),
                    ArticleSummary = table.Column<string>(nullable: false),
                    ArticleTitle = table.Column<string>(nullable: false),
                    ArticleViewCount = table.Column<long>(nullable: true),
                    IsOpenForComment = table.Column<bool>(nullable: false),
                    UserIDfk = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.ArticleId);
                    table.ForeignKey(
                        name: "FK_Articles_AspNetUsers_UserIDfk",
                        column: x => x.UserIDfk,
                        principalSchema: "MainDb",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArticleArticleTags",
                schema: "MainDb",
                columns: table => new
                {
                    ArticleId = table.Column<int>(nullable: false),
                    ArticleTagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleArticleTags", x => new { x.ArticleId, x.ArticleTagId });
                    table.ForeignKey(
                        name: "FK_ArticleArticleTags_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalSchema: "MainDb",
                        principalTable: "Articles",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleArticleTags_ArticleTags_ArticleTagId",
                        column: x => x.ArticleTagId,
                        principalSchema: "MainDb",
                        principalTable: "ArticleTags",
                        principalColumn: "ArticleTagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticleComments",
                schema: "MainDb",
                columns: table => new
                {
                    ArticleCommentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArticleCommentBody = table.Column<string>(nullable: false),
                    ArticleCommentDateCreated = table.Column<DateTime>(nullable: false),
                    ArticleCommentEmail = table.Column<string>(nullable: false),
                    ArticleCommentGravatar = table.Column<string>(nullable: true),
                    ArticleCommentName = table.Column<string>(nullable: false),
                    ArticleCommentParentId = table.Column<int>(nullable: true),
                    ArticleCommentWebSite = table.Column<string>(nullable: true),
                    ArticleIDfk = table.Column<int>(nullable: false),
                    IsCommentApproved = table.Column<bool>(nullable: false),
                    UserIDfk = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleComments", x => x.ArticleCommentId);
                    table.ForeignKey(
                        name: "FK_ArticleComments_ArticleComments_ArticleCommentParentId",
                        column: x => x.ArticleCommentParentId,
                        principalSchema: "MainDb",
                        principalTable: "ArticleComments",
                        principalColumn: "ArticleCommentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ArticleComments_Articles_ArticleIDfk",
                        column: x => x.ArticleIDfk,
                        principalSchema: "MainDb",
                        principalTable: "Articles",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleComments_AspNetUsers_UserIDfk",
                        column: x => x.UserIDfk,
                        principalSchema: "MainDb",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArticleRatings",
                schema: "MainDb",
                columns: table => new
                {
                    ArticleRatingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArticleIDfk = table.Column<int>(nullable: false),
                    ArticleRatingScore = table.Column<double>(nullable: false),
                    UserIDfk = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleRatings", x => x.ArticleRatingId);
                    table.ForeignKey(
                        name: "FK_ArticleRatings_Articles_ArticleIDfk",
                        column: x => x.ArticleIDfk,
                        principalSchema: "MainDb",
                        principalTable: "Articles",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleRatings_AspNetUsers_UserIDfk",
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
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "MainDb",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "MainDb",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "MainDb",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "MainDb",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                schema: "MainDb",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "MainDb",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "MainDb",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_UserIDfk",
                schema: "MainDb",
                table: "Articles",
                column: "UserIDfk");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleArticleTags_ArticleId",
                schema: "MainDb",
                table: "ArticleArticleTags",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleArticleTags_ArticleTagId",
                schema: "MainDb",
                table: "ArticleArticleTags",
                column: "ArticleTagId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleComments_ArticleCommentParentId",
                schema: "MainDb",
                table: "ArticleComments",
                column: "ArticleCommentParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleComments_ArticleIDfk",
                schema: "MainDb",
                table: "ArticleComments",
                column: "ArticleIDfk");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleComments_UserIDfk",
                schema: "MainDb",
                table: "ArticleComments",
                column: "UserIDfk");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleRatings_ArticleIDfk",
                schema: "MainDb",
                table: "ArticleRatings",
                column: "ArticleIDfk");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleRatings_UserIDfk",
                schema: "MainDb",
                table: "ArticleRatings",
                column: "UserIDfk");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "MainDb");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "MainDb");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "MainDb");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "MainDb");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "MainDb");

            migrationBuilder.DropTable(
                name: "ArticleArticleTags",
                schema: "MainDb");

            migrationBuilder.DropTable(
                name: "ArticleComments",
                schema: "MainDb");

            migrationBuilder.DropTable(
                name: "ArticleRatings",
                schema: "MainDb");

            migrationBuilder.DropTable(
                name: "Contacts",
                schema: "MainDb");

            migrationBuilder.DropTable(
                name: "Portfolios",
                schema: "MainDb");

            migrationBuilder.DropTable(
                name: "SiteOrders",
                schema: "MainDb");

            migrationBuilder.DropTable(
                name: "SlideShows",
                schema: "MainDb");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "MainDb");

            migrationBuilder.DropTable(
                name: "ArticleTags",
                schema: "MainDb");

            migrationBuilder.DropTable(
                name: "Articles",
                schema: "MainDb");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "MainDb");
        }
    }
}
