using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Domain.Interfaces.Cache
{
    public interface ICacheHandler
    {
        Task<byte[]> GetCacheValueAsync(string key);
        
        void SetCacheValueAsync(string key, byte[] data);
    }
}
