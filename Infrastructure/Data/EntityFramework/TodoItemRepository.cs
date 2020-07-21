using ApplicationCore.Application.Services.TodoLists.Queries.ExportTodos;
using ApplicationCore.Domain.Interfaces.Db;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Db;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data.EntityFramework
{
    public class TodoItemRepository : EfRepository<TodoItem>, ITodoItemRepository
    {
        public TodoItemRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<TodoItemRecord>> GetTodoItemRecords(int listId, IConfigurationProvider configurationProvider, CancellationToken cancellationToken)
        {
            var records = await _dbContext.TodoItems
                    .Where(t => t.ListId == listId)
                    .AsQueryable()
                    .ProjectTo<TodoItemRecord>(configurationProvider)
                    .ToListAsync(cancellationToken);

            return records;
        }
    }
}
