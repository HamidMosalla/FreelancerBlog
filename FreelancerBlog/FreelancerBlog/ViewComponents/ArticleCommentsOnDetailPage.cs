using System.Threading.Tasks;
using FreelancerBlog.Web.ViewModels.Article;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.Web.ViewComponents
{
    public class ArticleCommentsOnDetailPage : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(ArticleCommentTreeViewModel commentTree)
        {
            var articleCommentTree = new ArticleCommentTreeViewModel { CommentSeed = commentTree.CommentSeed, Comments = commentTree.Comments };

            return await Task.FromResult(View(articleCommentTree));
        }
    }
}