using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebFor.Web.Areas.Admin.ViewModels.Article
{
    public class ArticleTagViewModel
    {
        public int ArticleTagId { get; set; }

        [Display(Name ="نام تگ")]
        public string ArticleTagName { get; set; }
    }
}
