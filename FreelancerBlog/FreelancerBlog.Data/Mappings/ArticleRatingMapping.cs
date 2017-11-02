using FreelancerBlog.Core.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreelancerBlog.Data.Mappings
{
    internal class ArticleRatingMapping
    {
        internal static void Map(EntityTypeBuilder<ArticleRating> builder)
        {
            builder.Property(t => t.ArticleRatingScore).IsRequired();
            builder.Property(t => t.ArticleIDfk).IsRequired();
            builder.Property(t => t.UserIDfk).IsRequired();
        }
    }
}
