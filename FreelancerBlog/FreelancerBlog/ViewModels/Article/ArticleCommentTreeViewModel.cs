using System.Collections.Generic;
using FreelancerBlog.Core.DomainModels;

namespace FreelancerBlog.ViewModels.Article
{
    public class ArticleCommentTreeViewModel
    {
        public int? CommentSeed { get; set; }
        public ICollection<ArticleComment> Comments { get; set; }
    }
}
