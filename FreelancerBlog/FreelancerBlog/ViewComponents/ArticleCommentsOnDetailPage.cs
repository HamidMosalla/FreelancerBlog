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
        private IFreelancerBlogMapper _freelancerBlogMapper;

        public ArticleCommentsOnDetailPage(IUnitOfWork uw, IFreelancerBlogMapper freelancerBlogMapper)
        {
            _uw = uw;
            _freelancerBlogMapper = freelancerBlogMapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(ArticleCommentTreeViewModel commentTree)
        {
            var articleCommentTree = new ArticleCommentTreeViewModel { CommentSeed = commentTree.CommentSeed, Comments = commentTree.Comments };

            return View(articleCommentTree);
        }

    }
}