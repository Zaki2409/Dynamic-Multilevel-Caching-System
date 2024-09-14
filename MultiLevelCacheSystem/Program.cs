using MultiLevelCacheSystem.Cache;
using MultiLevelCacheSystem.Models;

class Program
{
    static void Main(string[] args)
    {
        var cacheManager = new CacheLevelManager();

        // Create cache levels with different capacities and eviction policies
        cacheManager.CreateCacheLevel(1, 2, new LRUCacheEvictionPolicy(2)); // Level 1: Capacity 2, LRU Policy
        cacheManager.CreateCacheLevel(2, 1, new LFUCacheEvictionPolicy(1)); // Level 2: Capacity 1, LFU Policy
        cacheManager.CreateCacheLevel(3, 1, new LRUCacheEvictionPolicy(1)); // Level 3: Capacity 1, LRU Policy
        cacheManager.CreateCacheLevel(4, 1, new LFUCacheEvictionPolicy(1)); // Level 4: Capacity 1, LFU Policy
        cacheManager.CreateCacheLevel(5, 1, new LRUCacheEvictionPolicy(1)); // Level 5: Capacity 1, LRU Policy

        // Adding real data to different cache levels using the Add method
        cacheManager.GetCacheLevel(1).Add("user123", new CacheItem("user123", "John Doe, johndoe@example.com"));
        cacheManager.GetCacheLevel(1).Add("user456", new CacheItem("user456", "Jane Smith, janesmith@example.com"));

        cacheManager.GetCacheLevel(2).Add("user789", new CacheItem("user789", "Alice Johnson, alicejohnson@example.com"));

        cacheManager.GetCacheLevel(3).Add("user101", new CacheItem("user101", "Bob Brown, bobbrown@example.com"));

        cacheManager.GetCacheLevel(4).Add("user102", new CacheItem("user102", "Carol Davis, caroldavis@example.com"));


        Console.WriteLine("Before caching");
        DisplayCacheStatus(cacheManager);
        // Fetching data and demonstrating cache management
        var item = cacheManager.FetchData("user789");
        Console.WriteLine($"Fetched item: {item?.Key ?? "Not found"}");


        Console.WriteLine("After     caching");
        // Display cache status
        DisplayCacheStatus(cacheManager);
    }



    static void DisplayCacheStatus(CacheLevelManager cacheManager)
    {
        // Example implementation to display cache levels status
        for (int i = 1; i <= 5; i++)
        {
            var level = cacheManager.GetCacheLevel(i);
            Console.WriteLine($"Cache Level {i}:");
            foreach (var item in level.GetAllItems())
            {
                Console.WriteLine($"  Key: {item.Key}, Value: {item.Value}");
            }
        }
    }
}
