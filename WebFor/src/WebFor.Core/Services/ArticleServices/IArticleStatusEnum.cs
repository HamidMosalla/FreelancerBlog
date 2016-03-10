using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFor.Core.Services.ArticleServices
{
    public enum ArticleStatus
    {
        ArticleCreateSucess = 0,
        ArticleTagCreateSucess = 1,
        ArticleArticleTagsCreateSucess = 2,
        ArticleEditSucess = 3,
        ArticleRemoveTagsFromArticleSucess = 4
    }

    public interface IArticleStatusEnum
    {
        ArticleStatus ArticleStatus { get; }
    }
}
