using System;

namespace LogsParser.Services.Contracts
{
    public interface IHttpCacheService
    {
        T Get<T>(string itemName, Func<T> getDataFunc, int durationInSeconds);

        void Remove(string itemName);
    }
}
