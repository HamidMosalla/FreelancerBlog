using FreelancerBlog.Core.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FreelancerBlog.Infrastructure.EntityFramework
{
    public class FreelancerBlogContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleComment> ArticleComments { get; set; }
        public DbSet<ArticleRating> ArticleRatings { get; set; }
        public DbSet<ArticleTag> ArticleTags { get; set; }
        public DbSet<ArticleArticleTag> ArticleArticleTags { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<SiteOrder> SiteOrders { get; set; }
        public DbSet<SlideShow> SlideShows { get; set; }

        public FreelancerBlogContext()
        {
            
        }

        public FreelancerBlogContext(DbContextOptions<FreelancerBlogContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=FreelancerBlogDb;Trusted_Connection=True;MultipleActiveResultSets=true");
            base.OnConfiguring(optionsBuilder); 
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            /*
                 adding migrations: dotnet ef migrations add Initial
                 running the migrations: dotnet ef database update
             */

            base.OnModelCreating(builder);

            builder.HasDefaultSchema("MainDb");

            #region part dealing with required fields 


            builder.Entity<Article>().Property(t => t.ArticleBody).IsRequired();
            builder.Entity<Article>().Property(t => t.ArticleDateCreated).IsRequired();
            builder.Entity<Article>().Property(t => t.ArticleSummary).IsRequired();
            builder.Entity<Article>().Property(t => t.ArticleTitle).IsRequired();
            builder.Entity<Article>().Property(t => t.UserIDfk).IsRequired();
            builder.Entity<Article>().Property(t => t.ArticleStatus).IsRequired();

            builder.Entity<ArticleComment>().Property(t => t.ArticleCommentName).IsRequired();
            builder.Entity<ArticleComment>().Property(t => t.ArticleCommentEmail).IsRequired();
            builder.Entity<ArticleComment>().Property(t => t.ArticleCommentDateCreated).IsRequired();
            builder.Entity<ArticleComment>().Property(t => t.ArticleIDfk).IsRequired();
            builder.Entity<ArticleComment>().Property(t => t.ArticleCommentBody).IsRequired();


            builder.Entity<ArticleRating>().Property(t => t.ArticleRatingScore).IsRequired();
            builder.Entity<ArticleRating>().Property(t => t.ArticleIDfk).IsRequired();
            builder.Entity<ArticleRating>().Property(t => t.UserIDfk).IsRequired();


            builder.Entity<ArticleTag>().Property(t => t.ArticleTagName).IsRequired();

            builder.Entity<Contact>().Property(t => t.ContactBody).IsRequired();
            builder.Entity<Contact>().Property(t => t.ContactDate).IsRequired();
            builder.Entity<Contact>().Property(t => t.ContactEmail).IsRequired();
            builder.Entity<Contact>().Property(t => t.ContactName).IsRequired();

            builder.Entity<Portfolio>().Property(t => t.PortfolioTitle).IsRequired();
            builder.Entity<Portfolio>().Property(t => t.PortfolioThumbnail).IsRequired();
            builder.Entity<Portfolio>().Property(t => t.PortfolioBody).IsRequired();

            builder.Entity<SiteOrder>().Property(t => t.SiteOrderFullName).IsRequired();
            builder.Entity<SiteOrder>().Property(t => t.SiteOrderEmail).IsRequired();
            builder.Entity<SiteOrder>().Property(t => t.SiteOrderPhone).IsRequired();
            builder.Entity<SiteOrder>().Property(t => t.SiteOrderDesc).IsRequired();

            builder.Entity<SlideShow>().Property(t => t.SlideShowTitle).IsRequired();
            builder.Entity<SlideShow>().Property(t => t.SlideShowPictrure).IsRequired();
            builder.Entity<SlideShow>().Property(t => t.SlideShowBody).IsRequired();
            builder.Entity<SlideShow>().Property(t => t.SlideShowDateCreated).IsRequired();

            //stupid mistake caused an entity validation error on migration, 
            //in this case I didn't provide any first name and last name ... when creating user.
            builder.Entity<ApplicationUser>().Property(t => t.UserFullName).IsRequired();
            builder.Entity<ApplicationUser>().Property(t => t.UserGender).IsRequired();
            builder.Entity<ApplicationUser>().Property(t => t.UserRegisteredDate).IsRequired();

            //it's here just for reference, you don't need to do this, just mark you type as nullable in your domain
            //model, or if it already is nullable, you don't need to do anything, it is automatically be set as nullable
            //in database
            //builder.Entity<ApplicationUser>().Property(t => t.UserBanEndDate).IsRequired(false);


            #endregion

            #region part dealing with relationships and referential integrity and cascading deletes

            //One User , many Article
            builder.Entity<ApplicationUser>()
                .HasMany(p => p.Articles)
                .WithOne(p => p.ApplicationUser)
                .HasForeignKey(p => p.UserIDfk)
                .OnDelete(DeleteBehavior.Restrict);

            //****************************************//
            //One User , many ArticleRating
            builder.Entity<ApplicationUser>()
                .HasMany(p => p.ArticleRatings)
                .WithOne(p => p.ApplicationUser)
                .HasForeignKey(p => p.UserIDfk)
                .OnDelete(DeleteBehavior.Restrict);

            //****************************************//
            //One user, many ArticleComment
            builder.Entity<ApplicationUser>()
                .HasMany(p => p.ArticleComments)
                .WithOne(p => p.ApplicationUser)
                .HasForeignKey(p => p.UserIDfk)
                .OnDelete(DeleteBehavior.Restrict);
            //****************************************//
            //One Article, many ArticleComment
            builder.Entity<Article>()
                .HasMany(p => p.ArticleComments)
                .WithOne(p => p.Article)
                .HasForeignKey(p => p.ArticleIDfk)
                .OnDelete(DeleteBehavior.Cascade);
            //****************************************//
            //One Article, many ArticleRating
            builder.Entity<Article>()
                .HasMany(p => p.ArticleRatings)
                .WithOne(p => p.Article)
                .HasForeignKey(p => p.ArticleIDfk)
                .OnDelete(DeleteBehavior.Cascade);
            //****************************************//
            //One Parent ArticleComment, many Child ArticleComment
            builder.Entity<ArticleComment>()
                   .HasMany(e => e.ArticleCommentChilds)
                   .WithOne(e => e.ArticleCommentParent)
                   .HasForeignKey(e => e.ArticleCommentParentId);
            //****************************************//
            //Setting a composite key for many to many rel between Article and Tag
            builder.Entity<ArticleArticleTag>().HasKey(x => new { x.ArticleId, x.ArticleTagId });
            //****************************************//

            #endregion
        }
    }
}
