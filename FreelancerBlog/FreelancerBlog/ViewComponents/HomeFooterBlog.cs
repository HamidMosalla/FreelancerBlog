using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FreelancerBlog.Areas.Admin.ViewModels.Article;
using FreelancerBlog.AutoMapper;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.ViewComponents
{
    public class HomeFooterBlog : ViewComponent
    {

        private IUnitOfWork _uw;
        private readonly IMapper _mapper;

        public HomeFooterBlog(IUnitOfWork uw)
        {
            _uw = uw;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var articles = await _uw.ArticleRepository.GetLatestArticles(3);

            var articlesViewModel = _mapper.Map<List<Article>, List<ArticleViewModel>>(articles);

            return View(articlesViewModel);
        }

    }
}
