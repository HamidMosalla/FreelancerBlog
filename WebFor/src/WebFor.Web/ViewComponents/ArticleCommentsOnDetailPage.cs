using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebFor.Core.Domain;
using WebFor.Core.Repository;
using WebFor.Web.Mapper;
using WebFor.Web.ViewModels.Article;

namespace WebFor.Web.ViewComponents
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
