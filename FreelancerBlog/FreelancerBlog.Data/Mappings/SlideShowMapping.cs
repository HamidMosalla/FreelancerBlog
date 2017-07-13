using FreelancerBlog.Core.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreelancerBlog.Data.Mappings
{
    internal class SlideShowMapping 
    {
        internal static void Map(EntityTypeBuilder<SlideShow> builder)
        {
            builder.Property(t => t.SlideShowTitle).IsRequired();
            builder.Property(t => t.SlideShowPictrure).IsRequired();
            builder.Property(t => t.SlideShowBody).IsRequired();
            builder.Property(t => t.SlideShowDateCreated).IsRequired();
        }
    }
}