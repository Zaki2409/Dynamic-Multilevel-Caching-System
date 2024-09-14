using MultiLevelCacheSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiLevelCacheSystem.Cache
{
    public class CacheLevel
    {
        public int LevelNumber { get; }
        private readonly int _capacity;
        private readonly IEvictionPolicy _evictionPolicy;
        private readonly Dictionary<string, CacheItem> _cache;

        public CacheLevel(int levelNumber, int capacity, IEvictionPolicy evictionPolicy)
        {
            LevelNumber = levelNumber;
            _capacity = capacity;
            _evictionPolicy = evictionPolicy;
            _cache = new Dictionary<string, CacheItem>();
        }

        public bool Contains(string key)
        {
            return _cache.ContainsKey(key);
        }

        public CacheItem Get(string key)
        {
            if (_cache.TryGetValue(key, out var item))
            {
                _evictionPolicy.OnAccess(key, item);
                return item;
            }
            return null;
        }

        public void Add(string key, CacheItem item)
        {
            if (_cache.Count >= _capacity)
            {
                var evictedKey = _evictionPolicy.Evict();
                if (evictedKey != null && _cache.TryGetValue(evictedKey, out var evictedItem))
                {
                    _cache.Remove(evictedKey);
                    _evictionPolicy.OnRemove(evictedKey, evictedItem);
                }
            }
            _cache[key] = item;
            _evictionPolicy.OnAdd(key, item);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
            _evictionPolicy.OnRemove(key, null);
        }

        public bool IsFull()
        {
            return _cache.Count >= _capacity;
        }

        public string Evict()
        {
            return _evictionPolicy.Evict();
        }

        public IEnumerable<CacheItem> GetAllItems()
        {
            return _cache.Values;
        }

    }
}
