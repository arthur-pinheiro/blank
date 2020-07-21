using ApplicationCore.Application.Services.TodoLists.Queries.ExportTodos;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Db;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationCore.Domain.Interfaces.Db
{
    public interface ITodoItemRepository : IAsyncRepository<TodoItem>
    {
        Task<IEnumerable<TodoItemRecord>> GetTodoItemRecords(int listId, IConfigurationProvider configurationProvider, CancellationToken cancellationToken);
    }
}
