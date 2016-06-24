using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;

namespace HQF.Tutorial.Redis.Function.UnitTest
{
   public class RedisContext:IDisposable
    {
        private readonly RedisClient redis = new RedisClient("localhost");

       public RedisClient RedisClient
       {
           get { return redis; }
       }

       public RedisContext()
       {
            redis.FlushAll();
       }


       public void Dispose()
        {
            //throw new NotImplementedException();
            redis.Dispose();
        }
    }
}
