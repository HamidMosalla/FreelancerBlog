using System;
using System.Collections.Generic;
using System.Text;
using FreelancerBlog.Core.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Org.BouncyCastle.Asn1.Ocsp;

namespace FreelancerBlog.Data.Mappings
{
    internal class ContactMapping 
    {
        internal static void Map(EntityTypeBuilder<Contact> builder)
        {
            builder.Property(t => t.ContactBody).IsRequired();
            builder.Property(t => t.ContactDate).IsRequired();
            builder.Property(t => t.ContactEmail).IsRequired();
            builder.Property(t => t.ContactName).IsRequired();
        }
    }
}