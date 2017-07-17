using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace FreelancerBlog.Core.Commands.Articles
{
    public class AddRatingToArticleCommand : IRequest
    {
        public int ArticleId { get; set; }
        public double ArticleRating { get; set; }
        public string UserId { get; set; }
    }
}