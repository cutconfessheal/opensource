using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Research.Core.Cache
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

        public T Get<T>(string key)
        {
            if (m_Cache == null)
                return default(T);
            if (m_Cache[key] == null)
                return default(T);
            return (T)m_Cache[key];
        }

        public T Add<T>(string uniqueKey, OnCacheItemAdding<T> action, int? seconds, bool isNeverExpire)
        {
            if(m_Cache != null)
            {
                if (m_Cache[uniqueKey] == null)
                {
                    T item = action.Invoke();
                    if (item != null)
                        m_Cache.Add(uniqueKey, item, null, DateTime.Now.AddSeconds(seconds.Value),
                                        System.Web.Caching.Cache.NoSlidingExpiration,
                                            ((isNeverExpire) ? System.Web.Caching.CacheItemPriority.NotRemovable : System.Web.Caching.CacheItemPriority.High), OnCacheItemRemovedCallback);
                }
            }
            return Get<T>(uniqueKey);
        }

        public void OnCacheItemRemovedCallback(string key, object sender, System.Web.Caching.CacheItemRemovedReason callback)
        {
            IDisposable disposable = sender as IDisposable;
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
                IDisposable disposable = m_Cache[key] as IDisposable;
                if (disposable != null)
                    disposable.Dispose();
                disposable = null;
                m_Cache[key] = null;
                m_Cache.Remove(key);
            }
        }

        public void Clean()
        {
            if (m_Cache == null)
                return;
            string[] keys = { };
            IDictionaryEnumerator enu = m_Cache.GetEnumerator();
            while (enu.MoveNext())
                keys.Union(new[] { enu.Key });
            foreach (string k in keys)
            {
                if (m_Cache[k] != null)
                {
                    IDisposable disposable = m_Cache[k] as IDisposable;
                    if (disposable != null)
                        disposable.Dispose();
                    disposable = null;
                    m_Cache[k] = null;
                    m_Cache.Remove(k);
                }
            }
        }
    }
}
