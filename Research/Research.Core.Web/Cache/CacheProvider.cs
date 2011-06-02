using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Research.Core.Web.Cache
{
    public class CacheProvider
    {
        public delegate T OnCacheItemAdding<T>();

        private System.Web.Caching.Cache m_Cache;

        public CacheProvider() : this(new System.Web.Caching.Cache()) { }
        
        public CacheProvider(System.Web.Caching.Cache cache)
        {
            m_Cache = cache;
        }

        public bool IsNull(string key)
        {
            return (m_Cache[key] == null);
        }

        public T Get<T>(string key)
        {
            if (m_Cache == null)
                return default(T);
            if (m_Cache[key] == null)
                return default(T);
            return (T)m_Cache[key];
        }

        public T Add<T>(string key, OnCacheItemAdding<T> action, int seconds, System.Web.Caching.CacheItemPriority priority)
        {
            if (seconds < 60)
                seconds = 60;
            return Add<T>(key, action, DateTime.Now.AddSeconds(seconds), priority);
        }

        public T Add<T>(string key, OnCacheItemAdding<T> action, DateTime expireAt, System.Web.Caching.CacheItemPriority priority)
        {
            if (m_Cache != null)
            {
                if (m_Cache[key] == null)
                {
                    if (expireAt < DateTime.Now)
                        expireAt = DateTime.Now.AddSeconds(60);
                    T item = action.Invoke();
                    if (item != null)
                        m_Cache.Add(key, item, null, expireAt, System.Web.Caching.Cache.NoSlidingExpiration, priority, OnCacheItemRemovedCallback);
                }
            }
            return Get<T>(key);
        }

        public void OnCacheItemRemovedCallback(string key, object sender, System.Web.Caching.CacheItemRemovedReason callback)
        {
            System.IDisposable disposable = sender as System.IDisposable;
            if (disposable != null)
                disposable.Dispose();
            disposable = null;
        }

        public void Remove(string key)
        {
            if (m_Cache == null)
                return;
            
            if (m_Cache[key] != null)
            {
                System.IDisposable disposable = m_Cache[key] as System.IDisposable;
                if (disposable != null)
                    disposable.Dispose();
                disposable = null;
                m_Cache[key] = null;
                m_Cache.Remove(key);
            }
        }

        public void Clear()
        {
            if (m_Cache == null)
                return;
            string[] keys = { };
            System.Collections.IDictionaryEnumerator enu = m_Cache.GetEnumerator();
            while (enu.MoveNext())
                keys = keys.Union(new[] { enu.Key.ToString() }).ToArray();
            foreach (string k in keys)
            {
                if (m_Cache[k] != null)
                {
                    System.IDisposable disposable = m_Cache[k] as System.IDisposable;
                    if (disposable != null)
                        disposable.Dispose();
                    m_Cache.Remove(k);
                }
            }
        }
    }
}
