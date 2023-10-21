using gRPC.Unary.Server.Helpers;
using gRPC.Unary.Server.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpc();
builder.Services.AddSingleton<IClock, Clock>();
builder.Services.AddSingleton<ITodoTasksService, TodoTasksService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<TodoGrpcService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();