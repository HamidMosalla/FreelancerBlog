using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebFor.Core.Domain;

namespace WebFor.Web.ViewModels.Article
{
    public class ArticleCommentViewModel
    {
        public ArticleCommentViewModel()
        {
            this.ArticleCommentDateCreated = DateTime.Now;
        }
        public int ArticleCommentId { get; set; }
        public int? ArticleCommentParentId { get; set; }
        public ArticleComment ArticleCommentParent { get; set; }
        public virtual ICollection<ArticleComment> ArticleCommentChilds { get; set; }

        [Display(Name = "تاریخ ارسال")]
        public DateTime ArticleCommentDateCreated { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage ="پر کردن نام اجباری میباشد.")]
        public string ArticleCommentName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage ="وارد کردن اجباری می باشد")]
        public string ArticleCommentEmail { get; set; }

        [Display(Name = "وب سایت")]
        public string ArticleCommentWebSite { get; set; }

        public string ArticleCommentGravatar { get; set; }

        [Display(Name = "متن نظر")]
        [Required(ErrorMessage ="پر کردن بدنه نظر اجباری می باشد")]
        public string ArticleCommentBody { get; set; }

        [Display(Name = "تایید شدن نظر")]
        public bool IsCommentApproved { get; set; }

        //[ForeignKey("UserIDfk")]   
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string UserIDfk { get; set; }

        public int ArticleIDfk { get; set; }
        //[ForeignKey("ArticleIDfk")]   
        public virtual WebFor.Core.Domain.Article Article { get; set; }
    }
}
