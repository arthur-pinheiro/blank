using ApplicationCore.Application.Services.Tests.Queries;
using ApplicationCore.Application.Services.TodoLists.Queries.ExportTodos;
using ApplicationCore.Domain.Entities;
using ApplicationCore.Domain.Interfaces.Db;
using ApplicationCore.Entities;
using ApplicationCore.Entities.Views;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        // Tables
        public DbSet<Client> Clients { get; set; }

        public DbSet<Contact> Contacts { get; set; }
        
        
        // Views
        public DbSet<VP_Client> VP_Clients { get; set; }
        
        public IEnumerable<TodoItem> TodoItemsIEnumerable { get; set; }

        public DbSet<TodoList> TodoLists { get; set; }

        public DbSet<TodoItem> TodoItems { get; set; }

        public DbSet<T_ServicoVeiculo> DatFromFunctions { get; set; }

        public async Task<IEnumerable<T_ServicoVeiculo>> GetData(long pempresa, string previsao, string pveiculoversao)
        {
            try
            {
                var data = await DatFromFunctions
                    .FromSqlRaw($"select * from getservicosveiculosobrigatorios3({pempresa}, '{previsao}', '{pveiculoversao}');")
                    .ToListAsync();

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // Essa é uma das formas possíveis de obter os registros (diretamente pelo contexto)
        public async Task<IEnumerable<TodoItemRecord>> GetTodoItemRecords(int listId, IConfigurationProvider configurationProvider, CancellationToken cancellationToken)
        {
            var records = await TodoItems
                    .Where(t => t.ListId == listId)
                    .AsQueryable()
                    .ProjectTo<TodoItemRecord>(configurationProvider)
                    .ToListAsync(cancellationToken);

            return records;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO: uncomment when changing model's table name
            //modelBuilder
            //    .Entity<Client>()
            //    .ToTable("db_clients");

            modelBuilder
                .Entity<VP_Client>()
                .HasNoKey();

            base.OnModelCreating(modelBuilder);
        }
    }
}
