using gRPC.Unary.Server.Helpers;
using gRPC.Unary.Server.Models;

namespace gRPC.Unary.Server.Services;

public class TodoTasksService : ITodoTasksService
{
    private readonly IClock _clock;
    private readonly List<TodoTask> _tasks;

    public TodoTasksService(IClock clock)
    {
        _clock = clock;
        _tasks = new List<TodoTask>()
        {
            new TodoTask(1, "Pranie", _clock.Current().AddDays(1)),
            new TodoTask(2, "Zmywanie", _clock.Current().AddDays(0)),
            new TodoTask(3, "Angielski", _clock.Current().AddDays(3)),
            new TodoTask(4, "Wyjazd na wakacje", _clock.Current().AddDays(10)),
        };
    }
    
    public TodoTask? GetById(int id)
    {
        return _tasks.FirstOrDefault(x => x.Id == id);
    }
}