using System;
using System.Collections.Generic;

namespace WebFor.Core.Domain
{
    public class ArticleComment
    {
        public ArticleComment()
        {
            ArticleCommentChilds = new HashSet<ArticleComment>();
        }

        public int ArticleCommentId { get; set; }
        public int? ArticleCommentParentId { get; set; }
        public ArticleComment ArticleCommentParent { get; set; }
        public virtual ICollection<ArticleComment> ArticleCommentChilds { get; set; }
        public DateTime ArticleCommentDateCreated { get; set; }
        public string ArticleCommentName { get; set; }
        public string ArticleCommentEmail { get; set; }
        public string ArticleCommentWebSite { get; set; }
        public string ArticleCommentGravatar { get; set; }
        public string ArticleCommentBody { get; set; }
        public bool IsCommentApproved { get; set; }

        //[ForeignKey("UserIDfk")]   
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string UserIDfk { get; set; }

        public int ArticleIDfk { get; set; }
        //[ForeignKey("ArticleIDfk")]   
        public virtual Article Article { get; set; }


    }
}
