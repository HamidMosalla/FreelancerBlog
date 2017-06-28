using System.Threading.Tasks;
using FreelancerBlog.Core.Repository;
using FreelancerBlog.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.ViewComponents
{
    public class HomeFooterBlog : ViewComponent
    {

        private IUnitOfWork _uw;
        private IFreelancerBlogMapper _freelancerBlogMapper;

        public HomeFooterBlog(IUnitOfWork uw, IFreelancerBlogMapper freelancerBlogMapper)
        {
            _uw = uw;
            _freelancerBlogMapper = freelancerBlogMapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var articles = await _uw.ArticleRepository.GetLatestArticles(3);

            var articlesViewModel = _freelancerBlogMapper.ArticleCollectionToArticleViewModelCollection(articles);

            return View(articlesViewModel);
        }

    }
}
