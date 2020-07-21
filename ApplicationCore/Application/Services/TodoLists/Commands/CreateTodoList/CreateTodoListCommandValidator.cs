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
    public class CreateTodoListCommandValidator : AbstractValidator<CreateTodoListCommand>
    {
        private readonly ITodoListRepository _todoListRepository;

        private readonly IApplicationDbContext _context;

        public CreateTodoListCommandValidator(IApplicationDbContext context, ITodoListRepository todoListRepository)
        {
            _context = context;
            _todoListRepository = todoListRepository;

            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
        }

        public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
        {
            // Qual o melhor padrão? usar o context ou sempre abstrair num repo?

            //return await _context.TodoLists
            //    .AllAsync(l => l.Title != title);


            var todoLists = await _todoListRepository
                .ListAllAsync();

            return todoLists.All(todoList => todoList.Title != title);
        }
    }
}
