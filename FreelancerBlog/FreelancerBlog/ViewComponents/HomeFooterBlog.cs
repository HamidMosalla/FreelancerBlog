using System.Threading.Tasks;
using FreelancerBlog.Core.Repository;
using FreelancerBlog.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.ViewComponents
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
