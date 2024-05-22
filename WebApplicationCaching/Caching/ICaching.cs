namespace WebApplicationCaching.Caching
{
    public interface ICaching
    {
        T GetData<T>(string key);
        bool SetData<T>(T data, string key, DateTimeOffset DateTimeOut);
        object RemoveData(string key);
    }
}
