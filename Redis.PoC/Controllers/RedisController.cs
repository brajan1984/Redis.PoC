using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceStack.Redis;
using Redis.PoC.Models;

namespace Redis.PoC.Controllers
{
    [Route("api/[controller]")]
    public class RedisController : Controller
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

        // POST api/values
        [HttpPost("[action]")]
        public void SetValue([FromBody] RedisValue value)
        {
            var manager = new RedisManagerPool(conn);
            using (var client = manager.GetClient())
            {
                client.Set(value.Key, value);
            }
        }

        [HttpPost("[action]")]
        public void Publish([FromBody] RedisChannel value)
        {
            var manager = new RedisManagerPool(conn);
            using (var client = manager.GetClient())
            {
                client.PublishMessage(value.ChannelId, value.Value);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
