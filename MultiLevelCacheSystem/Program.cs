using System;
using MultiLevelCacheSystem.Cache;
using MultiLevelCacheSystem.Models;

class Program
{
    private static CacheLevelManager cacheManager = new CacheLevelManager();

    static void Main(string[] args)
    {

       // Introduction();
        while (true)
        {
          
            Console.Clear();
            Introduction();
            Console.WriteLine("Cache System Menu:");
            Console.WriteLine("1. Run Sample Test Case");
            Console.WriteLine("2. Fetch Cache Item");
            Console.WriteLine("3. Display Cache Status");
            Console.WriteLine("4. Remove Cache Level");
            Console.WriteLine("5. Create Cache Level");
            Console.WriteLine("6. Add Cache Item");
            Console.WriteLine("7. Exit");
            Console.Write("Select an option (1-7): ");

            var choice = Console.ReadLine();
            switch (choice)
            {

                case "1":
                    RunSampleTests();
                    break;
                case "2":
                    FetchCacheItem();
                    break;
                case "3":
                    DisplayCacheStatus();
                    break;
                case "4":
                    RemoveCacheLevel();
                    break;
                case "5":
                    CreateCacheLevel();
                    break;
                case "6":
                    AddCacheItem();
                    break;
                case "7":
                    return; // Exit the application
                default:
                    Console.WriteLine("Invalid option. Please select a valid option.");
                    break;
            }

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }
    }


    static void RunSampleTests()
    {
        Console.WriteLine("Running Sample Tests...\n");

        // Create Cache Levels
        Console.WriteLine("Creating Cache Level 1 with capacity 2 and LRU eviction policy...");
        cacheManager.CreateCacheLevel(1, 2, new LRUCacheEvictionPolicy(2));

        Console.WriteLine("Creating Cache Level 2 with capacity 3 and LFU eviction policy...");
        cacheManager.CreateCacheLevel(2, 3, new LFUCacheEvictionPolicy(3));

        // Add Cache Items
        Console.WriteLine("Adding items to Cache Level 1...");
        AddCacheItem(1, "key1", "value1");
        AddCacheItem(1, "key2", "value2");
        AddCacheItem(1, "key3", "value3"); // This should cause the oldest item to be evicted based on LRU policy

        // Fetch Cache Items
        Console.WriteLine("Fetching items from Cache Level 1...");
        FetchCacheItem("key1");
        FetchCacheItem("key2");
        FetchCacheItem("key3");

        // Display Cache Status
        Console.WriteLine("Displaying Cache Status...");
        DisplayCacheStatus();

        // Remove Cache Level
        Console.WriteLine("Removing Cache Level 2...");
        RemoveCacheLevel(2);

        // Display Cache Status again to verify removal
        Console.WriteLine("Displaying Cache Status after removing Cache Level 2...");
        DisplayCacheStatus();
    }

    static void AddCacheItem(int level, string key, string value)
    {
        var item = new CacheItem(key, value);
        var cacheLevel = cacheManager.GetCacheLevel(level);
        if (cacheLevel != null)
        {
            cacheLevel.Add(key, item);
            Console.WriteLine($"Item added to Cache Level {level}: Key = {key}, Value = {value}");
        }
        else
        {
            Console.WriteLine($"Cache Level {level} not found.");
        }
    }

    static void FetchCacheItem(string key)
    {
        var item = cacheManager.FetchData(key);
        if (item != null)
        {
            Console.WriteLine($"Fetched item - Key: {item.Key}, Value: {item.Value}");
        }
        else
        {
            Console.WriteLine("Item not found.");
        }
    }

    static void RemoveCacheLevel(int level)
    {
        cacheManager.RemoveCacheLevel(level);
    }

    static void CreateCacheLevel()
    {
        Console.Write("Enter Cache Level Number: ");
        if (int.TryParse(Console.ReadLine(), out int levelNumber) && levelNumber > 0)
        {
            Console.Write("Enter Cache Capacity: ");
            if (int.TryParse(Console.ReadLine(), out int capacity) && capacity > 0)
            {
                Console.WriteLine("Select Eviction Policy:");
                Console.WriteLine("1. LRU (Least Recently Used)");
                Console.WriteLine("2. LFU (Least Frequently Used)");
                Console.Write("Enter your choice (1-2): ");

                IEvictionPolicy evictionPolicy = null;
                var policyChoice = Console.ReadLine();
                switch (policyChoice)
                {
                    case "1":
                        evictionPolicy = new LRUCacheEvictionPolicy(capacity);
                        break;
                    case "2":
                        evictionPolicy = new LFUCacheEvictionPolicy(capacity);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Defaulting to LRU policy.");
                        evictionPolicy = new LRUCacheEvictionPolicy(capacity);
                        break;
                }

                cacheManager.CreateCacheLevel(levelNumber, capacity, evictionPolicy);
                Console.WriteLine("Cache level created successfully.");
            }
            else
            {
                Console.WriteLine("Invalid capacity.");
            }
        }
        else
        {
            Console.WriteLine("Invalid cache level number.");
        }
    }

    static void AddCacheItem()
    {
        Console.Write("Enter Cache Level (1-5): ");
        if (int.TryParse(Console.ReadLine(), out int level) && level > 0)
        {
            Console.Write("Enter Key: ");
            var key = Console.ReadLine();
            Console.Write("Enter Value: ");
            var value = Console.ReadLine();
            var item = new CacheItem(key, value);
            cacheManager.GetCacheLevel(level)?.Add(key, item);
            Console.WriteLine("Item added successfully.");
        }
        else
        {
            Console.WriteLine("Invalid cache level.");
        }
    }

    static void FetchCacheItem()
    {
        Console.Write("Enter Key to fetch: ");
        var key = Console.ReadLine();
        var item = cacheManager.FetchData(key);
        if (item != null)
        {
            Console.WriteLine($"Fetched item - Key: {item.Key}, Value: {item.Value}");
        }
        else
        {
            Console.WriteLine("Item not found.");
        }
    }

    static void DisplayCacheStatus()
    {
        Console.WriteLine("Cache Status:");
        DisplayCacheStatus(cacheManager);
    }

    static void RemoveCacheLevel()
    {
        Console.Write("Enter Cache Level to remove: ");
        if (int.TryParse(Console.ReadLine(), out int level) && level > 0)
        {
            cacheManager.RemoveCacheLevel(level);
        }
        else
        {
            Console.WriteLine("Invalid cache level.");
        }
    }
    static void Introduction()
    {
        Console.WriteLine("Welcome to the Multi-Level Cache System!");
        Console.WriteLine("This application demonstrates a multi-level caching system with various cache levels and eviction policies.");
        Console.WriteLine("Here's a brief overview of the available functions:");
        Console.WriteLine("1. TO RUN SAMPLE TEST CASES FUCNTION");
        Console.WriteLine("2. Fetch Cache Item - Retrieves an item from the cache and promotes it to higher levels if applicable.");
        Console.WriteLine("3. Display Cache Status - Displays the current status and contents of all cache levels.");
        Console.WriteLine("4. Remove Cache Level - Removes a specified cache level from the system.");
        Console.WriteLine("5. Create Cache Level - Creates a new cache level with a specified capacity and eviction policy.");
        Console.WriteLine("6. Add Cache Item - Adds a new item to a specified cache level.");
        Console.WriteLine("7. Exit - Exits the application.");
        Console.WriteLine();
    }


    static void DisplayCacheStatus(CacheLevelManager cacheManager)
    {
        var levels = cacheManager.GetAllCacheLevels();

        if (levels.Count == 0)
        {
            Console.WriteLine("No cache levels available.");
            return;
        }

        foreach (var level in levels)
        {
            Console.WriteLine($"Cache Level {level.LevelNumber}:");

            foreach (var item in level.GetAllItems())
            {
                Console.WriteLine($"  Key: {item.Key}, Value: {item.Value}");
            }
        }
    }
}
