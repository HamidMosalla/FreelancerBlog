using System.Threading.Tasks;
using FreelancerBlog.Core.Repository;
using FreelancerBlog.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.ViewComponents
{
    public class ArticleTagBox : ViewComponent
    {

        private IUnitOfWork _uw;
        private IFreelancerBlogMapper _freelancerBlogMapper;

        public ArticleTagBox(IUnitOfWork uw, IFreelancerBlogMapper freelancerBlogMapper)
        {
            _uw = uw;
            _freelancerBlogMapper = freelancerBlogMapper;
        }

        public  async Task<IViewComponentResult> InvokeAsync()
        {
            var articleTags = await _uw.ArticleRepository.GetAllArticleTagsAsync();

            var articleTagViewModel = _freelancerBlogMapper.ArticleTagCollectionToArticleTagViewModelCollection(articleTags);

            return View(articleTagViewModel);
        }

    }
}
