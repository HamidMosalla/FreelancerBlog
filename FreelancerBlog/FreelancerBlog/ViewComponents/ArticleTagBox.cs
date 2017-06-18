using System.Threading.Tasks;
using FreelancerBlog.Core.Repository;
using FreelancerBlog.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerBlog.ViewComponents
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
            var articleTags = await _uw.ArticleRepository.GetAllArticleTagsAsync();

            var articleTagViewModel = _webForMapper.ArticleTagCollectionToArticleTagViewModelCollection(articleTags);

            return View(articleTagViewModel);
        }

    }
}
