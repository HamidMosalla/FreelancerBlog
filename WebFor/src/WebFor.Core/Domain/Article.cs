using System;
using System.Collections.Generic;

namespace WebFor.Core.Domain
{
    public class Article
    {
        public virtual ICollection<ArticleArticleTag> ArticleArticleTags { get; set; }
        public virtual ICollection<ArticleRating> ArticleRatings { get; set; }
        public virtual ICollection<ArticleComment> ArticleComments { get; set; }

        public int ArticleId { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleSummary { get; set; }
        public DateTime ArticleDateCreated { get; set; }
        public DateTime? ArticleDateModified { get; set; }
        public string ArticleBody { get; set; }
        public Int64? ArticleViewCount { get; set; }

        public string ArticleStatus { get; set; }

        public bool IsOpenForComment { get; set; }


        //[ForeignKey("ArticleUserIDfk")]   
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string UserIDfk { get; set; }


    }
}
