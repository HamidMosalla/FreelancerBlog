using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using WebFor.Infrastructure.EntityFramework;

namespace WebFor.Infrastructure.Migrations
{
    [DbContext(typeof(WebForDbContext))]
    [Migration("20160320064139_AddingGravatarToComment")]
    partial class AddingGravatarToComment
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("Relational:DefaultSchema", "MainDb")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasAnnotation("Relational:Name", "RoleNameIndex");

                    b.HasAnnotation("Relational:TableName", "AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasAnnotation("Relational:TableName", "AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasAnnotation("Relational:TableName", "AspNetUserRoles");
                });

            modelBuilder.Entity("WebFor.Core.Domain.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

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
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("UserOccupation");

                    b.Property<int?>("UserPoints");

                    b.Property<DateTime>("UserRegisteredDate");

                    b.Property<string>("UserSpeciality");

                    b.Property<string>("UserTwitterProfile");

                    b.Property<string>("UserWebSite");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasAnnotation("Relational:Name", "EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .HasAnnotation("Relational:Name", "UserNameIndex");

                    b.HasAnnotation("Relational:TableName", "AspNetUsers");
                });

            modelBuilder.Entity("WebFor.Core.Domain.Article", b =>
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

                    b.Property<string>("UserIDfk")
                        .IsRequired();

                    b.HasKey("ArticleId");
                });

            modelBuilder.Entity("WebFor.Core.Domain.ArticleArticleTag", b =>
                {
                    b.Property<int>("ArticleId");

                    b.Property<int>("ArticleTagId");

                    b.HasKey("ArticleId", "ArticleTagId");
                });

            modelBuilder.Entity("WebFor.Core.Domain.ArticleComment", b =>
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

                    b.Property<string>("UserIDfk");

                    b.HasKey("ArticleCommentId");
                });

            modelBuilder.Entity("WebFor.Core.Domain.ArticleRating", b =>
                {
                    b.Property<int>("ArticleRatingId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ArticleIDfk");

                    b.Property<double>("ArticleRatingScore");

                    b.Property<string>("UserIDfk")
                        .IsRequired();

                    b.HasKey("ArticleRatingId");
                });

            modelBuilder.Entity("WebFor.Core.Domain.ArticleTag", b =>
                {
                    b.Property<int>("ArticleTagId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ArticleTagName")
                        .IsRequired();

                    b.HasKey("ArticleTagId");
                });

            modelBuilder.Entity("WebFor.Core.Domain.Contact", b =>
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
                });

            modelBuilder.Entity("WebFor.Core.Domain.Portfolio", b =>
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
                });

            modelBuilder.Entity("WebFor.Core.Domain.SiteOrder", b =>
                {
                    b.Property<int>("SiteOrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("SiteOrderDesc")
                        .IsRequired();

                    b.Property<bool?>("SiteOrderDoesAdminManageUsers");

                    b.Property<bool?>("SiteOrderDoesHaveAdminSection");

                    b.Property<bool?>("SiteOrderDoesHaveAdvancedHtmlEditor");

                    b.Property<bool?>("SiteOrderDoesHaveCommenting");

                    b.Property<bool?>("SiteOrderDoesHaveComplexFooter");

                    b.Property<bool?>("SiteOrderDoesHaveDatabase");

                    b.Property<bool?>("SiteOrderDoesHaveDocumentation");

                    b.Property<bool?>("SiteOrderDoesHaveExternalAuth");

                    b.Property<bool?>("SiteOrderDoesHaveFeed");

                    b.Property<bool?>("SiteOrderDoesHaveFileManager");

                    b.Property<bool?>("SiteOrderDoesHaveMultipleCriteriaSearch");

                    b.Property<bool?>("SiteOrderDoesHaveNewsLetter");

                    b.Property<bool?>("SiteOrderDoesHaveRegistration");

                    b.Property<bool?>("SiteOrderDoesHaveSearch");

                    b.Property<bool?>("SiteOrderDoesHaveSearchAjax");

                    b.Property<bool?>("SiteOrderDoesHaveSeo");

                    b.Property<bool?>("SiteOrderDoesHaveShoppingCart");

                    b.Property<bool?>("SiteOrderDoesHaveShoppingCartWithoutRegister");

                    b.Property<bool?>("SiteOrderDoesHaveSiteMap");

                    b.Property<bool?>("SiteOrderDoesHaveSlideShow");

                    b.Property<bool?>("SiteOrderDoesHaveSsl");

                    b.Property<bool?>("SiteOrderDoesHaveUserProfile");

                    b.Property<bool?>("SiteOrderDoesIncludeChart");

                    b.Property<bool?>("SiteOrderDoesIncludeDomainAndHosting");

                    b.Property<bool?>("SiteOrderDoesIncludeDynamicChart");

                    b.Property<bool?>("SiteOrderDoesIncludeUnitTest");

                    b.Property<bool?>("SiteOrderDoesIncludeUserArea");

                    b.Property<bool?>("SiteOrderDoesSourceCodeIncluded");

                    b.Property<bool?>("SiteOrderDoesSuppoerIncluded");

                    b.Property<bool?>("SiteOrderDoesSupportCategory");

                    b.Property<bool?>("SiteOrderDoesSupportTagging");

                    b.Property<bool?>("SiteOrderDoesUseAjaxHeavy");

                    b.Property<bool?>("SiteOrderDoesUseAjaxLight");

                    b.Property<bool?>("SiteOrderDoesUseAjaxMedium");

                    b.Property<string>("SiteOrderEmail")
                        .IsRequired();

                    b.Property<string>("SiteOrderExample");

                    b.Property<decimal?>("SiteOrderFinalPrice");

                    b.Property<string>("SiteOrderFullName")
                        .IsRequired();

                    b.Property<string>("SiteOrderHowFindUs");

                    b.Property<bool?>("SiteOrderIsAsync");

                    b.Property<bool?>("SiteOrderIsCrossPlatform");

                    b.Property<bool?>("SiteOrderIsDynamic");

                    b.Property<bool?>("SiteOrderIsOptimizedForLightness");

                    b.Property<bool?>("SiteOrderIsResponsive");

                    b.Property<bool?>("SiteOrderIsSinglePage");

                    b.Property<string>("SiteOrderPhone")
                        .IsRequired();

                    b.Property<int?>("SiteOrderStaticPageNumber");

                    b.HasKey("SiteOrderId");
                });

            modelBuilder.Entity("WebFor.Core.Domain.SlideShow", b =>
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
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WebFor.Core.Domain.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WebFor.Core.Domain.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("WebFor.Core.Domain.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("WebFor.Core.Domain.Article", b =>
                {
                    b.HasOne("WebFor.Core.Domain.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserIDfk");
                });

            modelBuilder.Entity("WebFor.Core.Domain.ArticleArticleTag", b =>
                {
                    b.HasOne("WebFor.Core.Domain.Article")
                        .WithMany()
                        .HasForeignKey("ArticleId");

                    b.HasOne("WebFor.Core.Domain.ArticleTag")
                        .WithMany()
                        .HasForeignKey("ArticleTagId");
                });

            modelBuilder.Entity("WebFor.Core.Domain.ArticleComment", b =>
                {
                    b.HasOne("WebFor.Core.Domain.ArticleComment")
                        .WithMany()
                        .HasForeignKey("ArticleCommentParentId");

                    b.HasOne("WebFor.Core.Domain.Article")
                        .WithMany()
                        .HasForeignKey("ArticleIDfk");

                    b.HasOne("WebFor.Core.Domain.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserIDfk");
                });

            modelBuilder.Entity("WebFor.Core.Domain.ArticleRating", b =>
                {
                    b.HasOne("WebFor.Core.Domain.Article")
                        .WithMany()
                        .HasForeignKey("ArticleIDfk");

                    b.HasOne("WebFor.Core.Domain.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserIDfk");
                });
        }
    }
}
