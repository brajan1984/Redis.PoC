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
        // GET api/values/
        [HttpGet]
        public RedisValue Get()
        {
            var key = "Invite";


            var manager = new RedisManagerPool(conn);
            using (var client = manager.GetClient())
            {
                var val = client.Get<RedisValue>(key);

                return val;
            }
        }

        [HttpGet("[action]/{key}")]
        public RedisValue GetByKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                key = "Invite";
            }
            var manager = new RedisManagerPool(conn);
            using (var client = manager.GetClient())
            {
                return client.Get<RedisValue>(key);
            }
        }
        
        [HttpPost]
        public void AddToList([FromBody] RedisValue value)
        {
            var manager = new RedisManagerPool(conn);
            using (var client = manager.GetClient())
            {
                client.AddItemToList(value.Key, value.Value);
            }
        }

        [HttpGet]
        public List<string> GetList(string listId)
        {
            var manager = new RedisManagerPool(conn);
            using (var client = manager.GetClient())
            {
                return client.GetAllItemsFromList(listId);
            }
        }

        [HttpPost]
        public void AddToSet([FromBody] RedisValue value)
        {
            var manager = new RedisManagerPool(conn);
            using (var client = manager.GetClient())
            {
                client.AddItemToSet(value.Key, value.Value);
            }
        }

        [HttpGet]
        public HashSet<string> GetSet(string id)
        {
            var manager = new RedisManagerPool(conn);
            using (var client = manager.GetClient())
            {
                return client.GetAllItemsFromSet(id);
            }
        }


        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
