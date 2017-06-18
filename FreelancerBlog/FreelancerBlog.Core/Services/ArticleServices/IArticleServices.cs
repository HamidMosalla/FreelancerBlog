using System.Collections.Generic;
using System.Threading.Tasks;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Enums;

namespace FreelancerBlog.Core.Services.ArticleServices
{
    public interface IArticleServices
    {
        Task<List<ArticleStatus>> CreateNewArticleAsync(Article article, string articleTags);
        Task<List<ArticleStatus>> EditArticleAsync(Article article, string articleTags);
    }
}
