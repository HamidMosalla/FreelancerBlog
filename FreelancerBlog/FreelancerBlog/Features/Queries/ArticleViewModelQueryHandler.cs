using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FreelancerBlog.Areas.Admin.ViewModels.Article;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Queries.Data.ArticleTags;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FreelancerBlog.Features.Queries
{
    public class ArticleViewModelQueryHandler : IAsyncRequestHandler<ArticleViewModelQuery, ArticleViewModel>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public ArticleViewModelQueryHandler(IMapper mapper, UserManager<ApplicationUser> userManager, IMediator mediator)
        {
            _mapper = mapper;
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<ArticleViewModel> Handle(ArticleViewModelQuery message)
        {
            var articleViewModel = _mapper.Map<Article, ArticleViewModel>(message.Article);
            articleViewModel.ArticleTags = await _mediator.Send(new TagsByArticleIdQuery { ArticleId = message.Article.ArticleId }); ;
            articleViewModel.ArticleTagsList = await _mediator.Send(new GetCurrentArticleTagsQuery { ArticleId = message.Article.ArticleId });
            articleViewModel.SumOfRating = articleViewModel.ArticleRatings.Sum(a => a.ArticleRatingScore) / articleViewModel.ArticleRatings.Count;
            articleViewModel.CurrentUserRating = articleViewModel.ArticleRatings.SingleOrDefault(a => a.UserIDfk == _userManager.GetUserId(message.User));

            return articleViewModel;
        }
    }
}