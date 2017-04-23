using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redis.PoC.Models
{
    public class RedisChannel
    {
        public string ChannelId { get; set; }
        public string Value { get; set; }
    }
}
