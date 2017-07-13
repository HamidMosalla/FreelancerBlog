using FreelancerBlog.Core.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreelancerBlog.Data.Mappings
{
    internal class ArticleTagMapping
    {
        internal static void Map(EntityTypeBuilder<ArticleTag> builder)
        {
            builder.Property(t => t.ArticleTagName).IsRequired();
            
        }
    }
}