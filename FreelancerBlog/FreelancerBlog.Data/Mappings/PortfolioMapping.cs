using System;
using System.Collections.Generic;
using System.Text;
using FreelancerBlog.Core.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Org.BouncyCastle.Asn1.Ocsp;

namespace FreelancerBlog.Data.Mappings
{
    internal class PortfolioMapping
    {
        internal static void Map(EntityTypeBuilder<Portfolio> builder)
        {
            builder.Property(t => t.PortfolioTitle).IsRequired();
            builder.Property(t => t.PortfolioThumbnail).IsRequired();
            builder.Property(t => t.PortfolioBody).IsRequired();
        }
    }
}