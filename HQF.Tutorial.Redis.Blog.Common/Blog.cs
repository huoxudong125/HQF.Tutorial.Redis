using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HQF.Tutorial.Redis.Blog.Common
{
    public class Blog
    {
        public Blog()
        {
            this.Tags = new List<string>();
            this.BlogPostIds = new List<long>();
        }

        public long Id { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public List<string> Tags { get; set; }
        public List<long> BlogPostIds { get; set; }
    }
}
