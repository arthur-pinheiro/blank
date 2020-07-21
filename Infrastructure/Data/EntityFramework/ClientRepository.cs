using ApplicationCore.Domain.Interfaces.Db;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ClientRepository : EfRepository<Client>, IClientRepository
    {
        public ClientRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<Client> GetByIdWithItemsAsync(int id)
        {
            return _dbContext.Clients
                .Include(c => c.Contacts)
                .Include($"{nameof(Client.Contacts)}.{nameof(Contact.Email)}")
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
