using System;
using System.Collections.Generic;
using System.Text;
using FreelancerBlog.Core.Domain;
using MediatR;

namespace FreelancerBlog.Core.Commands.ArticleComments
{
    public class ToggleArticleCommentApprovalCommand : IRequest
    {
        public ArticleComment ArticleComment { get; set; }
    }
}