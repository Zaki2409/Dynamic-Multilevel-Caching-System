using MultiLevelCacheSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiLevelCacheSystem.Cache
{
    public class CacheLevelManager
    {
        private readonly Dictionary<int, CacheLevel> _cacheLevels = new Dictionary<int, CacheLevel>();

        public void RemoveCacheLevel(int level)
        {
            if (_cacheLevels.ContainsKey(level))
            {
                _cacheLevels.Remove(level);
                Console.WriteLine($"Cache level {level} removed.");
            }
            else
            {
                Console.WriteLine($"Cache level {level} not found.");
            }
        }




        // Create a cache level
        public void CreateCacheLevel(int levelNumber, int capacity, IEvictionPolicy evictionPolicy)
        {
            if (_cacheLevels.ContainsKey(levelNumber))
            {
                throw new InvalidOperationException("Cache level already exists.");
            }
            _cacheLevels[levelNumber] = new CacheLevel(levelNumber, capacity, evictionPolicy);
        }

        // Get a cache level by number
        public CacheLevel GetCacheLevel(int levelNumber)
        {
            if (_cacheLevels.TryGetValue(levelNumber, out var level))
            {
                return level;
            }
            //throw new KeyNotFoundException($"Cache level {levelNumber} not found.");
            // Instead of throwing an exception, return null if the level is not found
            return null;
        }

        // Fetch data from the cache
        public CacheItem FetchData(string key)
        {
            foreach (var level in _cacheLevels.Values)
            {
                if (level.Contains(key))
                {
                    var item = level.Get(key);
                    PromoteDataToUpperLevels(level, key, item);
                    return item;
                }
            }
            return null;
        }

        private void PromoteDataToUpperLevels(CacheLevel currentLevel, string key, CacheItem item)
        {
            if (currentLevel.LevelNumber == 1)
                return;

            currentLevel.Remove(key);

            foreach (var level in _cacheLevels.Values)
            {
                if (level.LevelNumber == currentLevel.LevelNumber - 1)
                {
                    if (level.IsFull())
                    {
                        var evictedKey = level.Evict();
                        var evictedItem = level.Get(evictedKey);
                        level.Add(key, item);

                        if (evictedItem != null)
                        {
                            AddToNextLevel(evictedItem);
                        }
                    }
                    else
                    {
                        level.Add(key, item);
                    }
                    break;
                }
            }
        }

        private void AddToNextLevel(CacheItem item)
        {
            foreach (var level in _cacheLevels.Values)
            {
                if (level.LevelNumber == 2)
                {
                    if (level.IsFull())
                    {
                        var evictedKey = level.Evict();
                        var evictedItem = level.Get(evictedKey);
                        level.Add(item.Key, item);

                        if (evictedItem != null)
                        {
                            AddToNextLevel(evictedItem);
                        }
                    }
                    else
                    {
                        level.Add(item.Key, item);
                    }
                    break;
                }
            }
        }

    }
}
