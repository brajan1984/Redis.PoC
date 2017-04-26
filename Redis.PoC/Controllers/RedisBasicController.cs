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
       

        [HttpGet]
        public RedisValue GetValue()
        {
            var manager = new RedisManagerPool(conn);
            using (var client = manager.GetClient())
            {
                return client.Get<RedisValue>("simpleKey");
            }
        }

        // POST api/values
        [HttpGet]
        public void SetValue()
        {
            var manager = new RedisManagerPool(conn);
            using (var client = manager.GetClient())
            {
                var value = new RedisValue { Key = "simpleKey", Value = "value1" };

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

        [HttpGet]
        public void AddToHash()
        {
            var manager = new RedisManagerPool(conn);
            using (var client = manager.GetClient())
            {
                client.SetEntryInHash("hash1", "key1", "value1");
                client.SetEntryInHash("hash1", "key2", "value2");
                client.SetEntryInHash("hash1", "key3", "value3");
                client.SetEntryInHash("hash1", "key4", "value4");
            }
        }

        [HttpGet]
        public void ModifyKeyInHash()
        {
            var manager = new RedisManagerPool(conn);
            using (var client = manager.GetClient())
            {
                client.SetEntryInHash("hash1", "key1", "value100");
            }
        }

        [HttpGet()]
        public Dictionary<string,string> GetFromHash()
        {
            var manager = new RedisManagerPool(conn);
            using (var client = manager.GetClient())
            {
                return client.GetAllEntriesFromHash("hash1");
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
