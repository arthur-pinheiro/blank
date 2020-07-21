using ApplicationCore.Domain.Entities;
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
    public class TodoListRepository : EfRepository<TodoList>, ITodoListRepository
    {
        public TodoListRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
