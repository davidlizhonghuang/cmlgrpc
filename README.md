#gRPC demo

## Rest and gRPC

REST is a triditional CRUD API using HTTP, gRPC using HTTP/2 is invented for high-performance communicaiton between service to service such as microservice. 

## Proto - (interface)
gRPC defines service contract in proto file, this proto contract needs to be implemented in a web service.

##
syntax = "proto3";

option csharp_namespace = "TodoGrpc";

package todo; //need to be the same as the namespace in the c# code todo.proto

service Todoer {  //crud
    //create
    rpc CreateToDo (CreateTodoRequest) returns (CreateTodoResponse){}
    //read single   
    rpc ReadTodo (ReadTodoRequest) returns (ReadTodoResponse){}
    //read all  
    rpc GetAllTodos (GetAllRequest) returns (ReadTodosResponse){}
    //read list
    rpc ReadTodosStream (ReadTodosRequest) returns (stream ReadTodosResponse){}
    //update
    rpc UpdateTodo (UpdateTodoRequest) returns (UpdateTodoResponse){}    
    //delete      
    rpc DeleteTodo (DeleteTodoRequest) returns (DeleteTodoResponse){}

}
message CreateTodoRequest{  //totally get from database, request response as message 
    string title = 1;
    string description = 2;
}
message CreateTodoResponse{
    int32 id = 1;
}
message ReadTodoRequest{
    int32 id = 1;
}   
message ReadTodoResponse{
    int32 id = 1;
    string title = 2;
    string description = 3;
    bool isDone = 4;
}
message GetAllRequest{
}

message ReadTodosRequest{

}

message ReadTodosResponse{
    int32 id = 1;
    string title = 2;
    string description = 3;
    bool isDone = 4;
}
message UpdateTodoRequest{
    int32 id = 1;
    string title = 2;
    string description = 3;
    bool isDone = 4;
}
message UpdateTodoResponse{
    int32 id = 1;
    string title = 2;
    string description = 3;
    bool isDone = 4;
}
message DeleteTodoRequest{
    int32 id = 1;
}
message DeleteTodoResponse{
    int32 id = 1;
    string title = 2;
    string description = 3;
    bool isDone = 4;
}




## Services
implement gRPC contract
## 
using Grpc.Core;
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

    }
}

## Test
postman is used to test this service call as API way.
![image](https://github.com/user-attachments/assets/8af9bfb5-a74a-40bf-8176-adfabb7a7969)


