using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFor.Models
{
    public class ArticleComment
    {
        public int ArticleCommentId { get; set; }
        public int? ArticleCommentParentId { get; set; }
        public DateTime ArticleDateCreated  { get; set; }
        public string ArticleCommentName { get; set; }
        public string ArticleCommentEmail { get; set; }
        public string ArticleCommentWebSite { get; set; }
        public string AricleCommentBody { get; set; }

        //[ForeignKey("UserIDfk")]   
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string UserIDfk { get; set; }

        public int ArticleIDfk { get; set; }
        //[ForeignKey("ArticleIDfk")]   
        public virtual Article Article { get; set; }


    }
}
