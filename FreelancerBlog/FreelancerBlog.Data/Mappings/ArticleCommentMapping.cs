using FreelancerBlog.Core.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreelancerBlog.Data.Mappings
{
    internal class ArticleCommentMapping
    {
        internal static void Map(EntityTypeBuilder<ArticleComment> builder)
        {
            builder.Property(t => t.ArticleCommentName).IsRequired();
            builder.Property(t => t.ArticleCommentEmail).IsRequired();
            builder.Property(t => t.ArticleCommentDateCreated).IsRequired();
            builder.Property(t => t.ArticleIDfk).IsRequired();
            builder.Property(t => t.ArticleCommentBody).IsRequired();

            builder.HasMany(e => e.ArticleCommentChilds)
                .WithOne(e => e.ArticleCommentParent)
                .HasForeignKey(e => e.ArticleCommentParentId);
        }
    }
}