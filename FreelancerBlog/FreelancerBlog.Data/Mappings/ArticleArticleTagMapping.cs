using FreelancerBlog.Core.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreelancerBlog.Data.Mappings
{
    internal class ArticleArticleTagMapping
    {
        internal static void Map(EntityTypeBuilder<ArticleArticleTag> builder)
        {
            builder.HasKey(x => new { x.ArticleId, x.ArticleTagId });
        }
    }
}