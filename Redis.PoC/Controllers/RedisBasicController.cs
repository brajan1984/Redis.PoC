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
    public class RedisBasicController : Controller
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

        [HttpGet("{key}")]
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

        // POST api/values
        [HttpPost]
        public void SetValue([FromBody] RedisValue value)
        {
            var manager = new RedisManagerPool(conn);
            using (var client = manager.GetClient())
            {
                client.Set(value.Key, value);
            }
        }

        [HttpPost]
        public void Publish([FromBody] RedisChannel value)
        {
            var manager = new RedisManagerPool(conn);
            using (var client = manager.GetClient())
            {
                client.PublishMessage(value.ChannelId, value.Value);
            }
        }

        [HttpPost]
        public void AddToHash([FromBody] RedisHash value)
        {
            var manager = new RedisManagerPool(conn);
            using (var client = manager.GetClient())
            {
                client.SetEntryInHash(value.HashId, value.Key, value.Value);
            }
        }

        [HttpGet("{hashId}")]
        public Dictionary<string,string> GetFromHash(string hashId)
        {
            var manager = new RedisManagerPool(conn);
            using (var client = manager.GetClient())
            {
                return client.GetAllEntriesFromHash(hashId);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
