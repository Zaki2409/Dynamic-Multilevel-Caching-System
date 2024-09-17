# Dynamic-Multilevel-Caching-System

# Project Requirements
.NET Core 8

C# Compiler
IDE (e.g., Visual Studio, Visual Studio Code)

# Installing Dependencies
1>Clone the Repository

2>Restore NuGet Packages


# Project Overview
This project demonstrates a multi-level caching system implemented in C#. The system supports two types of eviction policies:

LRU (Least Recently Used)

LFU (Least Frequently Used)

# Key Components
CacheItem: Represents an item in the cache with a Key and Value.

CacheLevel: Represents a level in the cache hierarchy, including methods for adding, fetching, and evicting items based on the eviction policy.

CacheLevelManager: Manages multiple cache levels, allowing for the creation, retrieval, and removal of cache levels, and promotes data between levels.

LRUCacheEvictionPolicy: Implements the LRU eviction policy.

LFUCacheEvictionPolicy: Implements the LFU eviction policy.

# File Structure
Program.cs: Contains the main entry point and the console application menu for interacting with the cache system.

CacheLevel.cs: Defines the CacheLevel class and its operations.

CacheLevelManager.cs: Manages cache levels and handles data promotion between levels.

CacheItem.cs: Defines the CacheItem class representing individual cache entries.

LRUCacheEvictionPolicy.cs: Implements the LRU eviction policy.

LFUCacheEvictionPolicy.cs: Implements the LFU eviction policy.



# How to Run the Application
1>dotnet build

2>dotnet run


# Interacting with the Application:

Once the application is running, you can use the console menu to:

Run Sample Test Case: Demonstrates the functionality of the cache system with predefined operations.

Fetch Cache Item: Retrieves an item from the cache.

Display Cache Status: Shows the status and contents of all cache levels.

Remove Cache Level: Removes a specific cache level.

Create Cache Level: Creates a new cache level with a specified capacity and eviction policy.

Add Cache Item: Adds a new item to a specified cache level.

Exit: Closes the application.



# Approach and Key Decisions
Cache Levels: The system supports multiple cache levels, each with its own capacity and eviction policy. This design allows for flexible cache management and better control over cache performance.

Eviction Policies: Implemented LRU and LFU eviction policies to handle cache item removal efficiently. LRU is suitable for scenarios where recently accessed items are more likely to be accessed again, while LFU is useful for scenarios where items accessed less frequently should be removed first.

Promotion of Data: When data is fetched from a lower-level cache, it is promoted to higher levels to improve access speed. The implementation ensures that data is efficiently managed across different cache levels.


# Best Practices
Separation of Concerns: Code is organized into distinct classes for managing different aspects of the caching system, ensuring clarity and maintainability.
