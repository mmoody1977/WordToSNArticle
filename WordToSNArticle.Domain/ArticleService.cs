using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordToSNArticle.Domain
{
    public class ArticleService : IArticle
    {
        public Article CreateArticle(string topic, string category, string shortDescription, string html)
        {
            Article newArticle = new Article { topic = topic, category = category, short_description = shortDescription, html = html };
            return newArticle;
        }
    }
}
