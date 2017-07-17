using System;
using System.Collections.Generic;
using System.Text;
using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Queries.ArticleComments
{
    public class ArticleCommentByIdQuery : IRequest<ArticleComment>
    {
        public int ArticleCommentId { get; set; }
    }
}