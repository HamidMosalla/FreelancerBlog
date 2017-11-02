using FreelancerBlog.Core.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FreelancerBlog.Data.Mappings
{
   internal class ApplicationUserMapping
    {
        internal static void Map(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(t => t.UserFullName).IsRequired();
            builder.Property(t => t.UserGender).IsRequired();
            builder.Property(t => t.UserRegisteredDate).IsRequired();

            builder.HasMany(p => p.Articles)
                .WithOne(p => p.ApplicationUser)
                .HasForeignKey(p => p.UserIDfk)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.ArticleRatings)
                .WithOne(p => p.ApplicationUser)
                .HasForeignKey(p => p.UserIDfk)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.ArticleComments)
                .WithOne(p => p.ApplicationUser)
                .HasForeignKey(p => p.UserIDfk)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}