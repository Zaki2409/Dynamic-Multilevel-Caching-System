using MultiLevelCacheSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiLevelCacheSystem.Cache
{
    public class LRUCacheEvictionPolicy : IEvictionPolicy
    {

        private readonly int _capacity;
        private readonly LinkedList<string> _accessOrder;
        private readonly Dictionary<string, CacheItem> _cache;

        public LRUCacheEvictionPolicy(int capacity)
        {
            _capacity = capacity;
            _cache = new Dictionary<string, CacheItem>();
            _accessOrder = new LinkedList<string>();
        }

        public void OnAdd(string key, CacheItem item)
        {
            if (_cache.Count >= _capacity)
            {
                Evict();
            }
            _cache[key] = item;
            _accessOrder.AddLast(key);
        }

        public void OnAccess(string key, CacheItem item)
        {
            if (_cache.ContainsKey(key))
            {
                _accessOrder.Remove(key);
                _accessOrder.AddLast(key);
            }
        }

        public void OnRemove(string key, CacheItem item)
        {
            _accessOrder.Remove(key);
        }

        public string Evict()
        {
            if (_accessOrder.Count == 0)
                return null;

            var oldestKey = _accessOrder.First.Value;
            _accessOrder.RemoveFirst();
            _cache.Remove(oldestKey);
            return oldestKey;

        }



    }
}

