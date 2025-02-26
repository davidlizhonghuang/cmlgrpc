using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using TodoGrpc.Data;
using TodoGrpcService.Models;


namespace TodoGrpc.Services
{
    public class TodoService: Todoer.TodoerBase //service contract and its base
    {
        private readonly AppDbContext _appDbContext;
        public TodoService(AppDbContext appDbContext){
            _appDbContext = appDbContext;
        }

        public override async Task<CreateTodoResponse> CreateToDo(CreateTodoRequest request, ServerCallContext context)
        {
            var todoItem = new ToDoItem
            {
                Title = request.Title,  
                Description = request.Description
            };

            _appDbContext.TodoItems.Add(todoItem);

            await _appDbContext.SaveChangesAsync();
       
           return await Task.FromResult(new CreateTodoResponse
            {
                Id = todoItem.Id
            });

        }

        // public override async Task<ReadTodosResponse> GetAllTodos(GetAllRequest request, ServerCallContext context)
        // {
        //     var todoItems = await _appDbContext.TodoItems.ToListAsync();

        //     var response = new ReadTodosResponse();

        //     // foreach (var todoItem in todoItems)
        //     // {
        //     //     response.Add(new ToDoItem{Id=todoItem.Id});
        //     // }

        //     return response;
        // } .......


    }
}