using ApplicationCore.Application.Services.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace ApplicationCore.Interfaces.File
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}