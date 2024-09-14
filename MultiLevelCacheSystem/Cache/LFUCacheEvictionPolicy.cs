using MultiLevelCacheSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiLevelCacheSystem.Cache
{
    public class LFUCacheEvictionPolicy : IEvictionPolicy
    {

        private readonly int _capacity;
        private readonly Dictionary<string, CacheItem> _cache;
        private readonly Dictionary<string, int> _frequency;

        public LFUCacheEvictionPolicy(int capacity)
        {
            _capacity = capacity;
            _cache = new Dictionary<string, CacheItem>();
            _frequency = new Dictionary<string, int>();
        }

        public void OnAdd(string key, CacheItem item)
        {
            if (_cache.Count >= _capacity)
            {
                Evict();
            }
            _cache[key] = item;
            _frequency[key] = 1;
        }

        public void OnAccess(string key, CacheItem item)
        {
            if (_cache.ContainsKey(key))
            {
                _frequency[key]++;
            }
        }

        public void OnRemove(string key, CacheItem item)
        {
            _frequency.Remove(key);
        }

        public string Evict()
        {
            if (_frequency.Count == 0)
                return null;

            var leastFrequentKey = _frequency.OrderBy(pair => pair.Value).First().Key;
            _cache.Remove(leastFrequentKey);
            _frequency.Remove(leastFrequentKey);
            return leastFrequentKey;
        }
    }

}

