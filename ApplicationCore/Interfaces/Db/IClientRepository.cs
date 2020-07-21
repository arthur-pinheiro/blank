using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Db;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IClientRepository : IAsyncRepository<Client>
    {
        Task<Client> GetByIdWithItemsAsync(int id);
    }
}