using gRPC.Unary.Server.Models;

namespace gRPC.Unary.Server.Services;

public interface ITodoTasksService
{
    TodoTask? GetById(int id);
}