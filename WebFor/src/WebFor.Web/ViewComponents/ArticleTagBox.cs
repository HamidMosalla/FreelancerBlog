using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebFor.Core.Domain;
using WebFor.Core.Repository;
using WebFor.Web.Services;
using WebFor.Web.ViewModels.Article;

namespace WebFor.Web.ViewComponents
{
    public class ArticleTagBox : ViewComponent
    {

        private IUnitOfWork _uw;
        private IWebForMapper _webForMapper;

        public ArticleTagBox(IUnitOfWork uw, IWebForMapper webForMapper)
        {
            _uw = uw;
            _webForMapper = webForMapper;
        }

        public  async Task<IViewComponentResult> InvokeAsync()
        {
            var articleTags = await _uw.ArticleTagRepository.GetAllAsync();

            var articleTagViewModel = _webForMapper.ArticleTagCollectionToArticleTagViewModelCollection(articleTags);

            return View(articleTagViewModel);
        }

    }
}
