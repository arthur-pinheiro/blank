using ApplicationCore.Application.Services.Tests.Queries;
using ApplicationCore.Application.Services.TodoLists.Queries.ExportTodos;
using ApplicationCore.Entities;
using AutoMapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationCore.Domain.Interfaces.Db
{
    public interface IApplicationDbContext
    {
        IEnumerable<TodoItem> TodoItemsIEnumerable { get; set; }

        Task<IEnumerable<TodoItemRecord>> GetTodoItemRecords(int listId, IConfigurationProvider configurationProvider, CancellationToken cancellationToken);

        Task<IEnumerable<T_ServicoVeiculo>> GetData(long pempresa, string previsao, string pveiculoversao);

        //DbSet<TodoList> TodoLists { get; set; }

        //DbSet<TodoItem> TodoItems { get; set; }

        //Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}