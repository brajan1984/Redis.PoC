using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceStack.Redis;
using Redis.PoC.Models;

namespace Redis.PoC.Controllers
{
    [Route("api/[controller]/[action]")]
    public class RedisCollectionsController : Controller
    {
        private readonly string conn = "localhost:6379";
        
        
        [HttpGet]
        public void AddToList()
        {
            var manager = new RedisManagerPool(conn);
            using (var client = manager.GetClient())
            {
                client.AddItemToList("simpleList", "value1");
                client.AddItemToList("simpleList", "value2");
                client.AddItemToList("simpleList", "value1");
                client.AddItemToList("simpleList", "value3");
                client.AddItemToList("simpleList", "value4");
            }
        }

        [HttpGet]
        public List<string> GetList()
        {
            var manager = new RedisManagerPool(conn);
            using (var client = manager.GetClient())
            {
                return client.GetAllItemsFromList("simpleList");
            }
        }

        [HttpGet]
        public void AddToSet()
        {
            var manager = new RedisManagerPool(conn);
            using (var client = manager.GetClient())
            {
                client.AddItemToSet("simpleSet", "value1");
                client.AddItemToSet("simpleSet", "value2");
                client.AddItemToSet("simpleSet", "value1");
                client.AddItemToSet("simpleSet", "value3");
                client.AddItemToSet("simpleSet", "value4");
            }
        }

        [HttpGet]
        public HashSet<string> GetSet()
        {
            var manager = new RedisManagerPool(conn);
            using (var client = manager.GetClient())
            {
                return client.GetAllItemsFromSet("simpleSet");
            }
        }

        [HttpGet]
        public void AddToSortedSet()
        {
            var manager = new RedisManagerPool(conn);
            using (var client = manager.GetClient())
            {
                client.AddItemToSortedSet("sortedSet", "value1", 5);
                client.AddItemToSortedSet("sortedSet", "value2", 1);
                client.AddItemToSortedSet("sortedSet", "value3", 40);
            }
        }

        [HttpGet]
        public List<string> GetSortedSet()
        {
            var manager = new RedisManagerPool(conn);
            using (var client = manager.GetClient())
            {
                return client.GetAllItemsFromSortedSet("sortedSet");
            }
        }


        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
