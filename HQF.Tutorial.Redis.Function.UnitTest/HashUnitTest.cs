using System;
using System.Linq;
using ServiceStack.Redis;
using Xunit;
using Xunit.Abstractions;

namespace HQF.Tutorial.Redis.Function.UnitTest
{
    public class HashUnitTest:IClassFixture<RedisContext>
    {
        private readonly RedisContext _redisContext;
        private readonly ITestOutputHelper _output;

        private string RedisKey
        {
            get { return "HQF:Redis:HashTest"; }
        }

        public HashUnitTest(RedisContext redisContext,ITestOutputHelper output)
        {
            _redisContext = redisContext;
            _output = output;
        }

        [Fact]
        public void TestIncrement()
        {
            using (var redisClient=_redisContext.RedisClient)
            {
                var key1 = "Key1";
                redisClient.SetEntryInHashIfNotExists(RedisKey,key1, "0");
                redisClient.IncrementValueInHash(RedisKey, key1, 1);

                var redisHash = redisClient.Hashes[RedisKey];

                Assert.True(redisHash.ContainsKey(key1));

                string value;
                redisHash.TryGetValue(key1, out value);
                Assert.Equal("1",value);

                redisClient.IncrementValueInHash(RedisKey, key1, 1);
                
                //不应该添加成功
                redisClient.SetEntryInHashIfNotExists(RedisKey, key1, "0");

                redisHash.TryGetValue(key1, out value);
                Assert.Equal("2", value);
            }
        }
    }
}
