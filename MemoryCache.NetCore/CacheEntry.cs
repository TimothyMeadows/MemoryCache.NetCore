using System;
using System.Runtime.InteropServices;

namespace MemoryCache.NetCore
{
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct CacheEntry<T>
    {
        public bool Serializable;
        public string Key;
        public T Value;
    }
}
