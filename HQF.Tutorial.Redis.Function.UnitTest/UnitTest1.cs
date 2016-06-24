using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.Redis;
using Xunit;
using Xunit.Abstractions;

namespace HQF.Tutorial.Redis.Function.UnitTest
{
    public class UnitTest1 : IClassFixture<RedisContext>
    {
        private readonly RedisContext _redisContext;
        private readonly ITestOutputHelper _output;

        private readonly string _sortedSetKey = "SortedSetKey";
        private readonly Random _random = new Random();
        private readonly int _ItemCount=100;


        private RedisClient RedisClient
        {
            get { return _redisContext.RedisClient; }
        }

        public UnitTest1(RedisContext redisContext, ITestOutputHelper output)
        {
            _redisContext = redisContext;
            this._output = output;

        }




        [Fact]
        public void TestAddToSortedSet()
        {
            for (int i = 0; i < _ItemCount; i++)
            {
                RedisClient.AddItemToSortedSet(_sortedSetKey, "key" + i, i+0.5f);
            }
        }

        /// <summary>
        /// 测试SortedSet 分页方式
        /// </summary>
        [Fact]
        public void TestGetIndexFromSortedSet()
        {
            var firstValue = ( _ItemCount -10);
            var firstKey = "key" +firstValue;
            var index =(int)RedisClient.GetItemIndexInSortedSetDesc(_sortedSetKey, firstKey);
            Assert.Equal(_ItemCount-firstValue-1, index);


         _output.WriteLine("Key is [{0}],Index[{1}]",firstKey,index);


            _output.WriteLine("\n=======Aesc");
            List<string> keysPageList = RedisClient.GetRangeFromSortedSet(_sortedSetKey, index, index+10);
            foreach (var key     in keysPageList)
            {
                _output.WriteLine(key);
            }

            _output.WriteLine("\n=======Desc");
            keysPageList = RedisClient.GetRangeFromSortedSetDesc(_sortedSetKey, index, index+10);
            foreach (var key in keysPageList)
            {
                _output.WriteLine(key);
            }

            var lastKey = "key" + 0;
            index = (int)RedisClient.GetItemIndexInSortedSet(_sortedSetKey, lastKey);
            Assert.Equal(0, index);

        }

    }
}
