using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.Data.ArticleTags;
using FreelancerBlog.Core.DomainModels;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Commands.ArticleTags
{
    class CreateArticleTagsCommandHandler : AsyncRequestHandler<CreateArticleTagsCommand>
    {
        private readonly FreelancerBlogContext _context;

        public CreateArticleTagsCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        protected override  Task Handle(CreateArticleTagsCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.ArticleTags)) return Task.FromResult(0);

            var newTags = request.ArticleTags.Split(',').Select(t => t.Trim()).ToList();
            var existingTags = _context.ArticleTags.ToList().Select(t => t.ArticleTagName.Trim());
            var tagsToAdd = newTags.Except(existingTags);

            var articleTags = tagsToAdd.Select(t => new ArticleTag { ArticleTagName = t }).ToList();
            _context.ArticleTags.AddRange(articleTags);

            var articleArticleTags = articleTags.Select(a => new ArticleArticleTag { Article = request.Article, ArticleTag = a });
            _context.ArticleArticleTags.AddRange(articleArticleTags);

            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}