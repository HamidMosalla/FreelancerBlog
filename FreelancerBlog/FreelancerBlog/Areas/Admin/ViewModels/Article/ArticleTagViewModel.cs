using System.ComponentModel.DataAnnotations;

namespace FreelancerBlog.Web.Areas.Admin.ViewModels.Article
{
    public class ArticleTagViewModel
    {
        public int ArticleTagId { get; set; }

        [Display(Name ="نام تگ")]
        public string ArticleTagName { get; set; }
    }
}
