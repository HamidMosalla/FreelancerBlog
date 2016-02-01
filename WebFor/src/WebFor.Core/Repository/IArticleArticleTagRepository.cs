using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFor.Core.Domain;

namespace WebFor.Core.Repository
{
    public interface IArticleArticleTagRepository : IRepository<ArticleArticleTag, int>
    {
        void AddRange(List<ArticleArticleTag> joinTableArtTagList);
    }
}
