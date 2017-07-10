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
    public class ArticleTagBox : ViewComponent
    {

        private IUnitOfWork _uw;
        private IFreelancerBlogMapper _freelancerBlogMapper;
        private readonly IMapper _mapper;

        public ArticleTagBox(IUnitOfWork uw, IFreelancerBlogMapper freelancerBlogMapper, IMapper mapper)
        {
            _uw = uw;
            _freelancerBlogMapper = freelancerBlogMapper;
            _mapper = mapper;
        }

        public  async Task<IViewComponentResult> InvokeAsync()
        {
            var articleTags = await _uw.ArticleRepository.GetAllArticleTagsAsync();

            var articleTagViewModel = _mapper.Map<List<ArticleTag>, List<ArticleTagViewModel>>(articleTags);

            return View(articleTagViewModel);
        }

    }
}
