using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFor.Core.Domain;

namespace WebFor.Web.ViewModels.Article
{
    public class ArticleCommentTreeViewModel
    {
        public int? CommentSeed { get; set; }
        public ICollection<ArticleComment> Comments { get; set; }
    }
}
