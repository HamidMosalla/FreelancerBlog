using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFor.Core.Domain;

namespace WebFor.Core.Services.ArticleServices
{
    public interface IArticleCreator : IArticleStatusEnum
    {
        Task<List<ArticleStatus>> CreateNewArticleAsync(Article article, string articleTags);
    }
}
