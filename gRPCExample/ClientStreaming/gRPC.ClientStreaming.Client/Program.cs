using gRPC.ClientStreaming.Server;
using Grpc.Net.Client;

Console.WriteLine("Provide \"start\" to begin");
string startCommand = Console.ReadLine();
if (startCommand == "start")
{
    Console.WriteLine("Provide number of answers");
    int answersNumber = int.Parse(Console.ReadLine());

    using var channel = GrpcChannel.ForAddress("http://localhost:5000");
    var client = new ExamResultsService.ExamResultsServiceClient(channel);

    using var call = client.SendResults();
    for (int i = 0; i < answersNumber; i++)
    {
        Console.WriteLine("For true provide 1 for false provide 0");
        int answer = int.Parse(Console.ReadLine());
        var request = new ExamTaskAnswerRequest()
        {
            TaskNumber = i + 1,
            Result = answer == 1
        };
        await call.RequestStream.WriteAsync(request);
    }

    await call.RequestStream.CompleteAsync();
    var response = await call.ResponseAsync;
    Console.WriteLine($"Summary {response.SummaryResult}%");
    foreach (var details in response.Details)
    {
        Console.WriteLine($"Task number : {details.TaskNumber} \nAnswer: {details.ProvidedResult} \nIs valid: {details.IsValid}");   
    }
}