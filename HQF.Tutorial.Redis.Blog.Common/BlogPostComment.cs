﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HQF.Tutorial.Redis.Blog.Common
{
    public class BlogPostComment
    {
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
