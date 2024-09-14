using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiLevelCacheSystem.Models
{
    public class CacheItem
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public CacheItem(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
