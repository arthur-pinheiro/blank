using ApplicationCore.Domain.Entities;
using ApplicationCore.Domain.Interfaces.Db;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationCore.Application.Services.TodoLists.Commands.CreateTodoList
{
    public partial class CreateTodoListCommand : IRequest<int>
    {
        public string Title { get; set; }
    }

    public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, int>
    {

        private readonly IApplicationDbContext _context;
        private readonly ITodoListRepository _todoListRepository;

        public CreateTodoListCommandHandler(IApplicationDbContext context, ITodoListRepository todoListRepository)
        {
            _context = context;
            _todoListRepository = todoListRepository;
        }

        public async Task<int> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
        {
            var entity = new TodoList
            {
                Title = request.Title
            };

            //_context.TodoLists.Add(entity);

            //await _context.SaveChangesAsync(cancellationToken);
            await _todoListRepository
                .SaveAsync(entity);

            return entity.Id;
        }
    }
}
