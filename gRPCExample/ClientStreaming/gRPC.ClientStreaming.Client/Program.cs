// See https://aka.ms/new-console-template for more information

using gRPC.ClientStreaming.Server;
using Grpc.Net.Client;

Console.WriteLine("Hello, World!");


using var channel = GrpcChannel.ForAddress("http://localhost:5000");
var client = new ExamResultsService.ExamResultsServiceClient(channel);

using var call = client.SendResults();
for (int i = 0; i < 10; i++)
{
    var request = new ExamTaskAnswerRequest()
    {
        TaskNumber = i + 1,
        Result = false
    };
    await call.RequestStream.WriteAsync(request);
}
await call.RequestStream.CompleteAsync();
var response = await call.ResponseAsync;
Console.WriteLine(response);