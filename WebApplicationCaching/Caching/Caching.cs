using System.Runtime.Caching;
using WebApplicationCaching.Data;

namespace WebApplicationCaching.Caching
{
    public class Caching : ICaching
    {
        public readonly EmployeeContext _context;
        public readonly ObjectCache _memoryCache = MemoryCache.Default;
        public Caching(EmployeeContext context)
        {
                _context = context;   
        }
        public T GetData<T>(string key)
        {
            T item = (T)_memoryCache.Get(key);
            return item;
        }

        public object RemoveData(string key)
        {
            try
            {
                if(!string.IsNullOrEmpty(key))
                {
                    _memoryCache.Remove(key);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public bool SetData<T>(T data, string key, DateTimeOffset DateTimeOut)
        {
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    _memoryCache.Set(key,data,DateTimeOut);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
