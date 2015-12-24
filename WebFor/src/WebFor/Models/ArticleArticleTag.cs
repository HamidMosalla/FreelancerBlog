using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFor.Models
{
    public class ArticleArticleTag
    {
        public int ArticleId { get; set; }
        public int ArticleTagId { get; set; }
        public Article Article { get; set; }
        public ArticleTag ArticleTag { get; set; }



    }
}
