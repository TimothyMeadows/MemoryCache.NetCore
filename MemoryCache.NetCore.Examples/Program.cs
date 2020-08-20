using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace MemoryCache.NetCore.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
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

            return;
        }
    }
}
