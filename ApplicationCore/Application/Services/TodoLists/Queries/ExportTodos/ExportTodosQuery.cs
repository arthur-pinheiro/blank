using ApplicationCore.Domain.Interfaces.Db;
using ApplicationCore.Interfaces.Db;
using ApplicationCore.Interfaces.File;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationCore.Application.Services.TodoLists.Queries.ExportTodos
{
    public class ExportTodosQuery : IRequest<ExportTodosVm>
    {
        public int ListId { get; set; }
    }

    public class ExportTodosQueryHandler : IRequestHandler<ExportTodosQuery, ExportTodosVm>
    {
        // Outra forma de busca dos todoitems, poderia ser através do repositório TodoItemRepo
        private readonly ITodoItemRepository _todoItemRepository;

        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICsvFileBuilder _fileBuilder;

        public ExportTodosQueryHandler(IApplicationDbContext context, IMapper mapper, ICsvFileBuilder fileBuilder, ITodoItemRepository todoItemRepository)
        {
            _context = context;
            _mapper = mapper;
            _fileBuilder = fileBuilder;
            _todoItemRepository = todoItemRepository;
        }

        public async Task<ExportTodosVm> Handle(ExportTodosQuery request, CancellationToken cancellationToken)
        {
            var vm = new ExportTodosVm();

            // TODO: No caso desse exemplo, o cara estava usando 'ToListAsync', q é um método do EntityFrameworkCore, ou seja, estava acoplando dependência de banco de dados diretamente aqui
            //var records = await _context.TodoItems
            //        .Where(t => t.ListId == request.ListId)
            //        .AsQueryable()
            //        .ProjectTo<TodoItemRecord>(_mapper.ConfigurationProvider)
            //        .ToListAsync(cancellationToken);

            IEnumerable<TodoItemRecord> todoItemRecords = await _context
                .GetTodoItemRecords(request.ListId, _mapper.ConfigurationProvider, cancellationToken);

            // Ou..
            IEnumerable<TodoItemRecord> todoItemRecordsFromRepo = await _todoItemRepository
                .GetTodoItemRecords(request.ListId, _mapper.ConfigurationProvider, cancellationToken);

            vm.Content = _fileBuilder.BuildTodoItemsFile(todoItemRecords);
            vm.ContentType = "text/csv";
            vm.FileName = "TodoItems.csv";

            return await Task.FromResult(vm);
        }
    }
}