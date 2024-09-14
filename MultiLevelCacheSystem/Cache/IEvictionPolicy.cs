using MultiLevelCacheSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiLevelCacheSystem.Cache
{
    public interface IEvictionPolicy
    {
        // Called when an item is added to the cache
        void OnAdd(string key, CacheItem item);

        // Called when an item is accessed
        void OnAccess(string key, CacheItem item);

        // Called when an item is removed from the cache
        void OnRemove(string key, CacheItem item);

        // Evicts an item according to the policy
        string Evict();
    }
}
