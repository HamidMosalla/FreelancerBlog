using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediatR;

namespace FreelancerBlog.Core.Queries.Articles
{
    public class ArticlesByTagQuery : IRequest<IQueryable<Domain.Article>>
    {
        public int TagId { get; set; }
    }
}
