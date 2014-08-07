using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordToSNArticle.Domain
{
    public interface IArticle
    {
        Article CreateArticle(string topic, string category, string shortDescription, string html);
    }
}
