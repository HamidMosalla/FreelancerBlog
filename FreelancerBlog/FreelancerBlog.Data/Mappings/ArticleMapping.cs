using FreelancerBlog.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreelancerBlog.Data.Mappings
{
    internal class ArticleMapping
    {
        internal static void Map(EntityTypeBuilder<Article> builder)
        {
            builder.Property(t => t.ArticleBody).IsRequired();
            builder.Property(t => t.ArticleDateCreated).IsRequired();
            builder.Property(t => t.ArticleSummary).IsRequired();
            builder.Property(t => t.ArticleTitle).IsRequired();
            builder.Property(t => t.UserIDfk).IsRequired();
            builder.Property(t => t.ArticleStatus).IsRequired();

            builder.HasMany(p => p.ArticleComments)
                .WithOne(p => p.Article)
                .HasForeignKey(p => p.ArticleIDfk)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.ArticleRatings)
                .WithOne(p => p.Article)
                .HasForeignKey(p => p.ArticleIDfk)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}