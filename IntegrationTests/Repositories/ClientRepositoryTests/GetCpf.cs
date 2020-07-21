using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Db;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests.Repositories.ClientRepositoryTests
{
    public class GetCpf
    {
        private ApplicationDbContext _clientContext;
        private IAsyncRepository<Client> _clientRepository;


        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestClient")
                .Options;

            _clientRepository = new ClientRepository(new ApplicationDbContext(dbOptions));
            var clientFromRepo = await _clientRepository.GetByIdAsync(1);

            Assert.AreEqual(clientFromRepo, null);
        }
    }
}
