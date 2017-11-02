using System;
using System.Collections.Generic;
using System.Text;
using FreelancerBlog.Core.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Org.BouncyCastle.Asn1.Ocsp;

namespace FreelancerBlog.Data.Mappings
{
    internal class SiteOrderMapping
    {
        internal static void Map(EntityTypeBuilder<SiteOrder> builder)
        {
            builder.Property(t => t.SiteOrderFullName).IsRequired();
            builder.Property(t => t.SiteOrderEmail).IsRequired();
            builder.Property(t => t.SiteOrderPhone).IsRequired();
            builder.Property(t => t.SiteOrderDesc).IsRequired();
        }
    }
}