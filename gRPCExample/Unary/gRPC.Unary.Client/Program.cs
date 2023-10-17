using System.Text.Json.Serialization;
using Grpc.Net.Client;
using gRPC.Unary.Server;

Console.WriteLine("Welcome. Please type task id. To quit app type \"quit\"");
string inputMessage = Console.ReadLine();

while (inputMessage is not "quit" && int.TryParse(inputMessage, out int id))
{
    using var channel = GrpcChannel.ForAddress("http://localhost:5000");
    var client = new TodoUnary.TodoUnaryClient(channel);
    var reply = await client.GetTaskAsync(new TodoRequest()
    {
        TaskId = id
    });
    if(reply is null)
        Console.WriteLine($"There is not task with Id: {id}");
    else
        Console.WriteLine($"ID: {reply.TaskId}\nContent: {reply.Content}\nEnd date: {reply.EndDateTime.ToDateTime()}");
    
    inputMessage = Console.ReadLine();
}