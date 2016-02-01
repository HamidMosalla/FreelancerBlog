using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFor.Core.Domain;

namespace WebFor.Core.Services.ArticleServices
{
    public interface IArticleCreator
    {
        Task<int> CreateNewArticle(Article article, string articleTags);
    }
}
