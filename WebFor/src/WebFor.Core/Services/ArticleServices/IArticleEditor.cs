using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFor.Core.Domain;
using WebFor.Core.Enums;

namespace WebFor.Core.Services.ArticleServices
{
    public interface IArticleEditor
    {
        Task<List<ArticleStatus>> EditArticleAsync(Article article, string articleTags);
    }
}
