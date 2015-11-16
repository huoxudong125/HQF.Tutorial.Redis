using System;
using System.Collections.Generic;
using System.Linq;
using HQF.Tutorial.Redis.Blog.Common;
using ServiceStack;
using ServiceStack.Redis;

namespace HQF.Tutorial.Redis.Blog.Imp.UnitTest
{
    //Shared Context between Tests
    //https://xunit.github.io/docs/shared-context.html


    public class BlogContext : IDisposable
    {
        private readonly RedisClient redis = new RedisClient("localhost");

        public BlogContext()
        {
            redis.FlushAll();
            InsertTestData();
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
            redis.Dispose();
        }

        public void InsertTestData()
        {
            var redisUsers = redis.As<User>();
            var redisBlogs = redis.As<Common.Blog>();
            var redisBlogPosts = redis.As<BlogPost>();

            var yangUser = new User {Id = redisUsers.GetNextSequence(), Name = "Eric Yang"};
            var zhangUser = new User {Id = redisUsers.GetNextSequence(), Name = "Fish Zhang"};

            var yangBlog = new Common.Blog
            {
                Id = redisBlogs.GetNextSequence(),
                UserId = yangUser.Id,
                UserName = yangUser.Name,
                Tags = new List<string> {"Architecture", ".NET", "Databases"}
            };

            var zhangBlog = new Common.Blog
            {
                Id = redisBlogs.GetNextSequence(),
                UserId = zhangUser.Id,
                UserName = zhangUser.Name,
                Tags = new List<string> {"Architecture", ".NET", "Databases"}
            };

            var blogPosts = new List<BlogPost>
            {
                new BlogPost
                {
                    Id = redisBlogPosts.GetNextSequence(),
                    BlogId = yangBlog.Id,
                    Title = "Memcache",
                    Categories = new List<string> {"NoSQL", "DocumentDB"},
                    Tags = new List<string> {"Memcache", "NoSQL", "JSON", ".NET"},
                    Comments = new List<BlogPostComment>
                    {
                        new BlogPostComment {Content = "First Comment!", CreatedDate = DateTime.UtcNow},
                        new BlogPostComment {Content = "Second Comment!", CreatedDate = DateTime.UtcNow}
                    }
                },
                new BlogPost
                {
                    Id = redisBlogPosts.GetNextSequence(),
                    BlogId = zhangBlog.Id,
                    Title = "Redis",
                    Categories = new List<string> {"NoSQL", "Cache"},
                    Tags = new List<string> {"Redis", "NoSQL", "Scalability", "Performance"},
                    Comments = new List<BlogPostComment>
                    {
                        new BlogPostComment {Content = "First Comment!", CreatedDate = DateTime.UtcNow}
                    }
                },
                new BlogPost
                {
                    Id = redisBlogPosts.GetNextSequence(),
                    BlogId = yangBlog.Id,
                    Title = "Cassandra",
                    Categories = new List<string> {"NoSQL", "Cluster"},
                    Tags = new List<string> {"Cassandra", "NoSQL", "Scalability", "Hashing"},
                    Comments = new List<BlogPostComment>
                    {
                        new BlogPostComment {Content = "First Comment!", CreatedDate = DateTime.UtcNow}
                    }
                },
                new BlogPost
                {
                    Id = redisBlogPosts.GetNextSequence(),
                    BlogId = zhangBlog.Id,
                    Title = "Couch Db",
                    Categories = new List<string> {"NoSQL", "DocumentDB"},
                    Tags = new List<string> {"CouchDb", "NoSQL", "JSON"},
                    Comments = new List<BlogPostComment>
                    {
                        new BlogPostComment {Content = "First Comment!", CreatedDate = DateTime.UtcNow}
                    }
                }
            };

            yangUser.BlogIds.Add(yangBlog.Id);
            yangBlog.BlogPostIds.AddRange(blogPosts.Where(x => x.BlogId == yangBlog.Id).Map(x => x.Id));

            zhangUser.BlogIds.Add(zhangBlog.Id);
            zhangBlog.BlogPostIds.AddRange(blogPosts.Where(x => x.BlogId == zhangBlog.Id).Map(x => x.Id));

            redisUsers.Store(yangUser);
            redisUsers.Store(zhangUser);
            redisBlogs.StoreAll(new[] {yangBlog, zhangBlog});
            redisBlogPosts.StoreAll(blogPosts);
        }
    }
}