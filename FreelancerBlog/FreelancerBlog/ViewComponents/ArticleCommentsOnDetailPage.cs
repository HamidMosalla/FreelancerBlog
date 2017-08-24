using System.Threading.Tasks;
using FreelancerBlog.ViewModels.Article;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.ViewComponents
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