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
    public class HomeFooterBlog : ViewComponent
    {

        private IUnitOfWork _uw;
        private IWebForMapper _webForMapper;

        public HomeFooterBlog(IUnitOfWork uw, IWebForMapper webForMapper)
        {
            _uw = uw;
            _webForMapper = webForMapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var articles = await _uw.ArticleRepository.GetLatestArticles(3);

            var articlesViewModel = _webForMapper.ArticleCollectionToArticleViewModelCollection(articles);

            return View(articlesViewModel);
        }

    }
}
