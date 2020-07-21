using ApplicationCore.Domain.Entities;
using ApplicationCore.Interfaces.Db;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Domain.Interfaces.Db
{
    public interface ITodoListRepository : IAsyncRepository<TodoList>
    {
    }
}
