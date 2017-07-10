using System.Threading.Tasks;
using FreelancerBlog.AutoMapper;
using FreelancerBlog.Core.Repository;
using FreelancerBlog.ViewModels.Article;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.ViewComponents
{
    public class ArticleCommentsOnDetailPage : ViewComponent
    {

        private IUnitOfWork _uw;

        public ArticleCommentsOnDetailPage(IUnitOfWork uw)
        {
            _uw = uw;
        }

        public async Task<IViewComponentResult> InvokeAsync(ArticleCommentTreeViewModel commentTree)
        {
            var articleCommentTree = new ArticleCommentTreeViewModel { CommentSeed = commentTree.CommentSeed, Comments = commentTree.Comments };

            return View(articleCommentTree);
        }

    }
}