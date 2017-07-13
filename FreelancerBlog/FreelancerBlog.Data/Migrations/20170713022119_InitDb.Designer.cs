using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using FreelancerBlog.Data.EntityFramework;

namespace FreelancerBlog.Data.Migrations
{
    [DbContext(typeof(FreelancerBlogContext))]
    [Migration("20170713022119_InitDb")]
    partial class InitDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasDefaultSchema("MainDb")
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FreelancerBlog.Core.Domain.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserAddress");

                    b.Property<string>("UserAvatar");

                    b.Property<DateTime?>("UserBanEndDate");

                    b.Property<string>("UserBio");

                    b.Property<DateTime?>("UserDateOfBirth");

                    b.Property<string>("UserFacebookProfile");

                    b.Property<string>("UserFavourites");

                    b.Property<string>("UserFullName")
                        .IsRequired();

                    b.Property<string>("UserGender")
                        .IsRequired();

                    b.Property<string>("UserGoogleProfile");

                    b.Property<string>("UserHowFindUs");

                    b.Property<string>("UserLinkedInProfile");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<string>("UserOccupation");

                    b.Property<string>("UserPhoneNumber");

                    b.Property<int?>("UserPoints");

                    b.Property<string>("UserProfileEmail");

                    b.Property<DateTime>("UserRegisteredDate");

                    b.Property<string>("UserSpeciality");

                    b.Property<string>("UserTwitterProfile");

                    b.Property<string>("UserWebSite");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("FreelancerBlog.Core.Domain.Article", b =>
                {
                    b.Property<int>("ArticleId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ArticleBody")
                        .IsRequired();

                    b.Property<DateTime>("ArticleDateCreated");

                    b.Property<DateTime?>("ArticleDateModified");

                    b.Property<string>("ArticleStatus")
                        .IsRequired();

                    b.Property<string>("ArticleSummary")
                        .IsRequired();

                    b.Property<string>("ArticleTitle")
                        .IsRequired();

                    b.Property<long?>("ArticleViewCount");

                    b.Property<bool>("IsOpenForComment");

                    b.Property<string>("UserIDfk")
                        .IsRequired();

                    b.HasKey("ArticleId");

                    b.HasIndex("UserIDfk");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("FreelancerBlog.Core.Domain.ArticleArticleTag", b =>
                {
                    b.Property<int>("ArticleId");

                    b.Property<int>("ArticleTagId");

                    b.HasKey("ArticleId", "ArticleTagId");

                    b.HasIndex("ArticleTagId");

                    b.ToTable("ArticleArticleTags");
                });

            modelBuilder.Entity("FreelancerBlog.Core.Domain.ArticleComment", b =>
                {
                    b.Property<int>("ArticleCommentId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ArticleCommentBody")
                        .IsRequired();

                    b.Property<DateTime>("ArticleCommentDateCreated");

                    b.Property<string>("ArticleCommentEmail")
                        .IsRequired();

                    b.Property<string>("ArticleCommentGravatar");

                    b.Property<string>("ArticleCommentName")
                        .IsRequired();

                    b.Property<int?>("ArticleCommentParentId");

                    b.Property<string>("ArticleCommentWebSite");

                    b.Property<int>("ArticleIDfk");

                    b.Property<bool>("IsCommentApproved");

                    b.Property<string>("UserIDfk");

                    b.HasKey("ArticleCommentId");

                    b.HasIndex("ArticleCommentParentId");

                    b.HasIndex("ArticleIDfk");

                    b.HasIndex("UserIDfk");

                    b.ToTable("ArticleComments");
                });

            modelBuilder.Entity("FreelancerBlog.Core.Domain.ArticleRating", b =>
                {
                    b.Property<int>("ArticleRatingId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ArticleIDfk");

                    b.Property<double>("ArticleRatingScore");

                    b.Property<string>("UserIDfk")
                        .IsRequired();

                    b.HasKey("ArticleRatingId");

                    b.HasIndex("ArticleIDfk");

                    b.HasIndex("UserIDfk");

                    b.ToTable("ArticleRatings");
                });

            modelBuilder.Entity("FreelancerBlog.Core.Domain.ArticleTag", b =>
                {
                    b.Property<int>("ArticleTagId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ArticleTagName")
                        .IsRequired();

                    b.HasKey("ArticleTagId");

                    b.ToTable("ArticleTags");
                });

            modelBuilder.Entity("FreelancerBlog.Core.Domain.Contact", b =>
                {
                    b.Property<int>("ContactId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContactBody")
                        .IsRequired();

                    b.Property<DateTime>("ContactDate");

                    b.Property<string>("ContactEmail")
                        .IsRequired();

                    b.Property<string>("ContactName")
                        .IsRequired();

                    b.Property<string>("ContactPhone");

                    b.HasKey("ContactId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("FreelancerBlog.Core.Domain.Portfolio", b =>
                {
                    b.Property<int>("PortfolioId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PortfolioBody")
                        .IsRequired();

                    b.Property<string>("PortfolioCategory");

                    b.Property<DateTime>("PortfolioDateBuilt");

                    b.Property<DateTime>("PortfolioDateCreated");

                    b.Property<string>("PortfolioSiteAddress");

                    b.Property<string>("PortfolioThumbnail")
                        .IsRequired();

                    b.Property<string>("PortfolioTitle")
                        .IsRequired();

                    b.HasKey("PortfolioId");

                    b.ToTable("Portfolios");
                });

            modelBuilder.Entity("FreelancerBlog.Core.Domain.SiteOrder", b =>
                {
                    b.Property<int>("SiteOrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("SiteOrderDesc")
                        .IsRequired();

                    b.Property<string>("SiteOrderDevelopmentComplexity");

                    b.Property<bool>("SiteOrderDoesAdminManageUsers");

                    b.Property<bool>("SiteOrderDoesConformToSolidDesign");

                    b.Property<bool>("SiteOrderDoesContentOnUs");

                    b.Property<bool>("SiteOrderDoesHaveAdminSection");

                    b.Property<bool>("SiteOrderDoesHaveAdvancedHtmlEditor");

                    b.Property<bool>("SiteOrderDoesHaveBlog");

                    b.Property<bool>("SiteOrderDoesHaveCommenting");

                    b.Property<bool>("SiteOrderDoesHaveComplexFooter");

                    b.Property<bool>("SiteOrderDoesHaveDatabase");

                    b.Property<bool>("SiteOrderDoesHaveDocumentation");

                    b.Property<bool>("SiteOrderDoesHaveExternalAuth");

                    b.Property<bool>("SiteOrderDoesHaveFaq");

                    b.Property<bool>("SiteOrderDoesHaveFeed");

                    b.Property<bool>("SiteOrderDoesHaveFileManager");

                    b.Property<bool>("SiteOrderDoesHaveForum");

                    b.Property<bool>("SiteOrderDoesHaveImageGallery");

                    b.Property<bool>("SiteOrderDoesHaveNewsLetter");

                    b.Property<bool>("SiteOrderDoesHaveRatinging");

                    b.Property<bool>("SiteOrderDoesHaveRegistration");

                    b.Property<bool>("SiteOrderDoesHaveSearch");

                    b.Property<bool>("SiteOrderDoesHaveSeo");

                    b.Property<bool>("SiteOrderDoesHaveShoppingCart");

                    b.Property<bool>("SiteOrderDoesHaveShoppingCartWithoutRegister");

                    b.Property<bool>("SiteOrderDoesHaveSiteMap");

                    b.Property<bool>("SiteOrderDoesHaveSlideShow");

                    b.Property<bool>("SiteOrderDoesHaveSsl");

                    b.Property<bool>("SiteOrderDoesHaveUserProfile");

                    b.Property<bool>("SiteOrderDoesIncludeAdvancedReport");

                    b.Property<bool>("SiteOrderDoesIncludeChart");

                    b.Property<bool>("SiteOrderDoesIncludeDomainAndHosting");

                    b.Property<bool>("SiteOrderDoesIncludeDynamicChart");

                    b.Property<bool>("SiteOrderDoesIncludeUnitTest");

                    b.Property<bool>("SiteOrderDoesIncludeUserArea");

                    b.Property<bool>("SiteOrderDoesSourceCodeIncluded");

                    b.Property<bool>("SiteOrderDoesSupportCategory");

                    b.Property<bool>("SiteOrderDoesSupportIncluded");

                    b.Property<bool>("SiteOrderDoesSupportTagging");

                    b.Property<bool>("SiteOrderDoesUseAjax");

                    b.Property<string>("SiteOrderDoesUseAjaxType");

                    b.Property<bool>("SiteOrderDoesUseCustomDesign");

                    b.Property<bool>("SiteOrderDoesUseGoogleAnalytics");

                    b.Property<bool>("SiteOrderDoesUseGoogleMap");

                    b.Property<bool>("SiteOrderDoesUseSocialMedia");

                    b.Property<bool>("SiteOrderDoesUseTemplates");

                    b.Property<bool>("SiteOrderDoesVideoPlaying");

                    b.Property<bool>("SiteOrderDoesWithOkWithJavascriptDisabled");

                    b.Property<string>("SiteOrderEmail")
                        .IsRequired();

                    b.Property<string>("SiteOrderExample");

                    b.Property<decimal?>("SiteOrderFinalPrice");

                    b.Property<string>("SiteOrderFullName")
                        .IsRequired();

                    b.Property<string>("SiteOrderHowFindUs");

                    b.Property<bool>("SiteOrderIsAsync");

                    b.Property<bool>("SiteOrderIsCrossPlatform");

                    b.Property<bool>("SiteOrderIsOptimizedForAccessibility");

                    b.Property<bool>("SiteOrderIsOptimizedForLightness");

                    b.Property<bool>("SiteOrderIsOptimizedForMobile");

                    b.Property<bool>("SiteOrderIsResponsive");

                    b.Property<bool>("SiteOrderIsSinglePage");

                    b.Property<string>("SiteOrderNumberOfMockUp");

                    b.Property<string>("SiteOrderPhone")
                        .IsRequired();

                    b.Property<string>("SiteOrderSearchType");

                    b.Property<string>("SiteOrderSeoType");

                    b.Property<int?>("SiteOrderStaticPageNumber");

                    b.Property<string>("SiteOrderSupportType");

                    b.Property<string>("SiteOrderTimeToDeliverMonth");

                    b.Property<string>("SiteOrderWebSiteType");

                    b.HasKey("SiteOrderId");

                    b.ToTable("SiteOrders");
                });

            modelBuilder.Entity("FreelancerBlog.Core.Domain.SlideShow", b =>
                {
                    b.Property<int>("SlideShowId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("SlideShowBody")
                        .IsRequired();

                    b.Property<DateTime>("SlideShowDateCreated");

                    b.Property<string>("SlideShowLink");

                    b.Property<string>("SlideShowPictrure")
                        .IsRequired();

                    b.Property<int?>("SlideShowPriority");

                    b.Property<string>("SlideShowTitle")
                        .IsRequired();

                    b.HasKey("SlideShowId");

                    b.ToTable("SlideShows");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("FreelancerBlog.Core.Domain.Article", b =>
                {
                    b.HasOne("FreelancerBlog.Core.Domain.ApplicationUser", "ApplicationUser")
                        .WithMany("Articles")
                        .HasForeignKey("UserIDfk");
                });

            modelBuilder.Entity("FreelancerBlog.Core.Domain.ArticleArticleTag", b =>
                {
                    b.HasOne("FreelancerBlog.Core.Domain.Article", "Article")
                        .WithMany("ArticleArticleTags")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FreelancerBlog.Core.Domain.ArticleTag", "ArticleTag")
                        .WithMany("ArticleArticleTags")
                        .HasForeignKey("ArticleTagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FreelancerBlog.Core.Domain.ArticleComment", b =>
                {
                    b.HasOne("FreelancerBlog.Core.Domain.ArticleComment", "ArticleCommentParent")
                        .WithMany("ArticleCommentChilds")
                        .HasForeignKey("ArticleCommentParentId");

                    b.HasOne("FreelancerBlog.Core.Domain.Article", "Article")
                        .WithMany("ArticleComments")
                        .HasForeignKey("ArticleIDfk")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FreelancerBlog.Core.Domain.ApplicationUser", "ApplicationUser")
                        .WithMany("ArticleComments")
                        .HasForeignKey("UserIDfk");
                });

            modelBuilder.Entity("FreelancerBlog.Core.Domain.ArticleRating", b =>
                {
                    b.HasOne("FreelancerBlog.Core.Domain.Article", "Article")
                        .WithMany("ArticleRatings")
                        .HasForeignKey("ArticleIDfk")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FreelancerBlog.Core.Domain.ApplicationUser", "ApplicationUser")
                        .WithMany("ArticleRatings")
                        .HasForeignKey("UserIDfk");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("FreelancerBlog.Core.Domain.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("FreelancerBlog.Core.Domain.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FreelancerBlog.Core.Domain.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
