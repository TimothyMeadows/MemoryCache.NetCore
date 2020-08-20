# MemoryCache.NetCore
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT) [![nuget](https://img.shields.io/nuget/v/MemoryCache.NetCore.svg)](https://www.nuget.org/packages/MemoryCache.NetCore/)

Implementation of a parallel thread-safe in-memory caching system with save, and load support suited for 'state' programming. 

# Install

From a command prompt
```bash
dotnet add package MemoryCache.NetCore
```

```bash
Install-Package MemoryCache.NetCore
```

You can also search for package via your nuget ui / website:

https://www.nuget.org/packages/MemoryCache.NetCore/

# Examples

You can find more examples in the github examples project.

```csharp
// You can populate the cache at initialization time
using var cache = new MemoryCache
{
  ["caw"] = "caw caw caw"
};

// You can also access an entry directly by key. However you will need to cast from dynamic using this method.
var caw1 = (string)cache["caw"];
cache["caw"] = "caw caw caw 2";

// You can create new entries directly by key, and they can be complex types!
cache["object"] = new KeyValuePair<string, int>("test", 42);

// However, non-serializable complex types will be "ignored" during .Save
cache["stream"] = new MemoryStream(new byte[] { 32, 34 });

// You can also use the Read<T>, and Write<T> methods. These methods handle casting for you to and from dynamic.
var caw2 = cache.Read<string>("caw");
cache.Write("caw", "caw caw caw 3");

// You can save cache using either Binary, or Json serialization by specifying either byte[], or string as T during save.
var binary = cache.Save<byte[]>();
var json = cache.Save<string>();

// You can reload cache from another source such as disk, or socket. With the option of clearing any existing data.
cache.Load(binary);
```

# Constructor

```csharp
MemoryCache()
```

# Methods

Write a dynamic value to cache without returning anything
```csharp
void Write(string key, dynamic value)
```

Write a T value to cache returning the T value from cache
```csharp
T Write<T>(string key, T value)
```

Read a value from cache returning as T
```csharp
T Read<T>(string key)
```

Delete an entry from cache without returning anything
```csharp
void Delete(string key)
```

Delete an entry from cache returning that value as T
```csharp
T Delete<T>(string key)
```

Serialize all entries in cache marked as serializable. If you specify T as byte[] binary serialization is used. If you specify T as string json serialization is used.
```csharp
T Save<T>()
```

Load serialized entries into cache. If you specify T as byte[] binary serialization is used. If you specify T as string json serialization is used.
```csharp
void Load<T>(T data, bool clear = true)
```
