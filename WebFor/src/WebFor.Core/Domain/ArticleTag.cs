using System.Collections.Generic;

namespace WebFor.Core.Domain
{
    public class ArticleTag
    {
        public virtual ICollection<ArticleArticleTag> ArticleArticleTags { get; set; }

        public int ArticleTagId { get; set; }
        public string ArticleTagName { get; set; }

    }
}
