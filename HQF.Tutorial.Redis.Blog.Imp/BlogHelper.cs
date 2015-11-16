using System.Collections.Generic;
using HQF.Tutorial.Redis.Blog.Common;
using ServiceStack.Redis;

namespace HQF.Tutorial.Redis.Blog
{
    public class BlogHelper
    {
        private readonly RedisClient redis = new RedisClient("localhost");

        public IList<Common.Blog> GetAkkBlogPosts()
        {
            var redisBlogs = redis.As<Common.Blog>();
            var blogs = redisBlogs.GetAll();
            return blogs;
        }

        //public List<BlogPost> GetAllBlogPosts()
        //{

        //    var redisBlogPosts = redis.As<BlogPost>();
        //    var newIncomingBlogPosts = redisBlogPosts.GetAll();
        //    var blogPosts = redisBlogPosts.Lists["urn:BlogPost:RecentPosts"];

        //}

    }
}