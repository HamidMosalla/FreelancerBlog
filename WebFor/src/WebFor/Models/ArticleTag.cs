using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFor.Models
{
    public class ArticleTag
    {
        public ArticleTag()
        {
            this.Articles = new HashSet<Article>();
        }

        public virtual ICollection<Article> Articles { get; set; }


        public int ArticleTagId { get; set; }
        public string ArticleTagName { get; set; }

    }
}
