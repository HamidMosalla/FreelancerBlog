using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFor.Core.Domain;
using WebFor.Core.Enums;

namespace WebFor.Core.Services.ArticleServices
{
    public interface IArticleServices
    {
        Task<List<ArticleStatus>> CreateNewArticleAsync(Article article, string articleTags);
        Task<List<ArticleStatus>> EditArticleAsync(Article article, string articleTags);
    }
}
