using System.Threading.Tasks;
using FreelancerBlog.Core.Repository;
using FreelancerBlog.Mapper;
using FreelancerBlog.ViewModels.Article;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.ViewComponents
{
    public class ArticleCommentsOnDetailPage : ViewComponent
    {

        private IUnitOfWork _uw;
        private IWebForMapper _webForMapper;

        public ArticleCommentsOnDetailPage(IUnitOfWork uw, IWebForMapper webForMapper)
        {
            _uw = uw;
            _webForMapper = webForMapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(ArticleCommentTreeViewModel commentTree)
        {
            var articleCommentTree = new ArticleCommentTreeViewModel { CommentSeed = commentTree.CommentSeed, Comments = commentTree.Comments };

            return View(articleCommentTree);
        }

    }
}