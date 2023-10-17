using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace gRPC.Unary.Server.Services;

public class TodoGrpcService : TodoUnary.TodoUnaryBase
{
    private readonly ITodoTasksService _todoTasksService;

    public TodoGrpcService(ITodoTasksService todoTasksService)
    {
        _todoTasksService = todoTasksService;
    }
    
    public override Task<TodoResponse> GetTask(TodoRequest request, ServerCallContext context)
    {
        var responseTask = _todoTasksService.GetById(request.TaskId);
        return responseTask is null ?
            Task.FromResult(new TodoResponse()) : 
            Task.FromResult(new TodoResponse()
            {
                TaskId = responseTask.Id,
                Content = responseTask.Content,
                EndDateTime = responseTask.EndTime.ToTimestamp()
            });
    }
}