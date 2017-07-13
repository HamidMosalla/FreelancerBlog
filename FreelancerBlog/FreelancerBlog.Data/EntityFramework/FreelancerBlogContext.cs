using FreelancerBlog.Core.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FreelancerBlog.Data.Mappings;

namespace FreelancerBlog.Data.EntityFramework
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

        public FreelancerBlogContext(){ }

        public FreelancerBlogContext(DbContextOptions<FreelancerBlogContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=FreelancerBlogDb;Trusted_Connection=True;MultipleActiveResultSets=true");
            base.OnConfiguring(optionsBuilder); 
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("MainDb");

            ApplicationUserMapping.Map(builder.Entity<ApplicationUser>());
            ArticleArticleTagMapping.Map(builder.Entity<ArticleArticleTag>());
            ArticleCommentMapping.Map(builder.Entity<ArticleComment>());
            ArticleMapping.Map(builder.Entity<Article>());
            ArticleRatingMapping.Map(builder.Entity<ArticleRating>());
            ArticleTagMapping.Map(builder.Entity<ArticleTag>());
            ContactMapping.Map(builder.Entity<Contact>());
            PortfolioMapping.Map(builder.Entity<Portfolio>());
            SiteOrderMapping.Map(builder.Entity<SiteOrder>());
            SlideShowMapping.Map(builder.Entity<SlideShow>());
        }
    }
}