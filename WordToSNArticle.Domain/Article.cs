using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordToSNArticle.Domain
{
    public class Article
    {
        public int sys_id { get; set; }
        public string category { get; set; }
        public string topic { get; set; }
        public string html { get; set; }
        public string short_description { get; set; }
    }
}
