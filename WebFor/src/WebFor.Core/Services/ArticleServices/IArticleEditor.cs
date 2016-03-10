using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFor.Core.Domain;

namespace WebFor.Core.Services.ArticleServices
{
    public interface IArticleEditor : IArticleStatusEnum
    {
        Task<List<ArticleStatus>> EditArticleAsync(Article article, string articleTags);
    }
}
