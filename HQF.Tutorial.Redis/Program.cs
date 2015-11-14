﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;

namespace HQF.Tutorial.Redis
{
    class Program
    {
      
        static void Main(string[] args)
        {
            Console.WriteLine("\nTest String");
            TestString();

            Console.WriteLine("\nTest Hash");
            TestHash();

            Console.WriteLine("\nTest List");
            TestList();

            Console.WriteLine("\nTest Set");
            TestSet();

            Console.WriteLine("\nTest SortSet");
            TestSortSet();

            Console.ReadKey();
        }

        private static void TestString()
        {
            using (var client = new RedisClient("127.0.0.1", 6379))
            {
                #region "字符串类型"
                client.Set<string>("name", "Reis");
                string userName = client.Get<string>("name");
                Console.WriteLine(userName);

                //访问次数
                client.Set<int>("IpAccessCount", 0);
                //次数递增
                client.Incr("IpAccessCount");
                Console.WriteLine(client.Get<int>("IpAccessCount"));
                #endregion
            }

        }

        private static void TestHash()
        {
            using (var client = new RedisClient("127.0.0.1", 6379))
            {
                client.SetEntryInHash("userInfoId", "name", "zhangsan");
                client.SetEntryInHash("userInfoId", "name1", "zhangsan1");
                client.SetEntryInHash("userInfoId", "name2", "zhangsan2");
                client.SetEntryInHash("userInfoId", "name3", "zhangsan3");
                client.GetHashKeys("userInfoId").ForEach(e => Console.WriteLine(e));
                client.GetHashValues("userInfoId").ForEach(e => Console.WriteLine(e));
            }
        }

        private static void TestList()
        {
            using (var client = new RedisClient("127.0.0.1", 6379))
            {
                #region "List类型"

                client.AddItemToList("userInfoId1", "123");
                client.AddItemToList("userInfoId1", "1234");

                Console.WriteLine("List数据项条数:" + client.GetListCount("userInfoId1"));
                Console.WriteLine("List数据项第一条数据:" + client.GetItemFromList("userInfoId1", 0));
                Console.WriteLine("List所有数据");
                client.GetAllItemsFromList("userInfoId1").ForEach(e => Console.WriteLine(e));
                #endregion

                #region "List类型做为队列和栈使用"
                Console.WriteLine(client.GetListCount("userInfoId1"));
                //队列先进先出
                //Console.WriteLine(client.DequeueItemFromList("userInfoId1"));
                //Console.WriteLine(client.DequeueItemFromList("userInfoId1"));

                //栈后进先出
                Console.WriteLine("出栈" + client.PopItemFromList("userInfoId1"));
                Console.WriteLine("出栈" + client.PopItemFromList("userInfoId1"));
                #endregion
            }
        }

        private static void TestSet()
        {
            using (var client = new RedisClient("127.0.0.1", 6379))
            {
                client.AddItemToSet("A", "B");
                client.AddItemToSet("A", "C");
                client.AddItemToSet("A", "D");
                client.AddItemToSet("A", "E");
                client.AddItemToSet("A", "F");

                client.AddItemToSet("B", "C");
                client.AddItemToSet("B", "F");

                //求差集
                Console.WriteLine("A,B集合差集");
                client.GetDifferencesFromSet("A", "B").ToList<string>().ForEach(e => Console.Write(e + ","));

                //求集合交集
                Console.WriteLine("\nA,B集合交集");
                client.GetIntersectFromSets(new string[] { "A", "B" }).ToList<string>().ForEach(e => Console.Write(e + ","));

                //求集合并集
                Console.WriteLine("\nA,B集合并集");
                client.GetUnionFromSets(new string[] { "A", "B" }).ToList<string>().ForEach(e => Console.Write(e + ","));
            }
        }


        private static void TestSortSet()
        {
            using (var client = new RedisClient("127.0.0.1", 6379))
            {
            
            #region "有序Set操作"
            client.AddItemToSortedSet("SA", "B", 2);
            client.AddItemToSortedSet("SA", "C", 1);
            client.AddItemToSortedSet("SA", "D", 5);
            client.AddItemToSortedSet("SA", "E", 3);
            client.AddItemToSortedSet("SA", "F", 4);

            //有序集合降序排列
            Console.WriteLine("\n有序集合降序排列");
            client.GetAllItemsFromSortedSetDesc("SA").ForEach(e => Console.Write(e + ","));
            Console.WriteLine("\n有序集合升序序排列");
            client.GetAllItemsFromSortedSet("SA").ForEach(e => Console.Write(e + ","));

            client.AddItemToSortedSet("SB", "C", 2);
            client.AddItemToSortedSet("SB", "F", 1);
            client.AddItemToSortedSet("SB", "D", 3);

            Console.WriteLine("\n获得某个值在有序集合中的排名，按分数的升序排列");
            Console.WriteLine(client.GetItemIndexInSortedSet("SB", "D"));

            Console.WriteLine("\n获得有序集合中某个值得分数");
            Console.WriteLine(client.GetItemScoreInSortedSet("SB", "D"));

            Console.WriteLine("\n获得有序集合中，某个排名范围的所有值");
            client.GetRangeFromSortedSet("SA", 0, 3).ForEach(e => Console.Write(e + ","));

                #endregion
            }
        }
    }
}
