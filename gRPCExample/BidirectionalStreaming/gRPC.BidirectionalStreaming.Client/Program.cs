using Grpc.Net.Client;
using gRPC.BidirectionalStreaming.Server;

Console.WriteLine("Please type \"start\" to begin stream");
string input = Console.ReadLine();



if (input is "start")
{
    using var channel = GrpcChannel.ForAddress("http://localhost:5000");
    var client = new PositionService.PositionServiceClient(channel);
    using var call = client.ShowResults();
    string answer = string.Empty;

    while (answer != "q")
    {
        Console.WriteLine("Read direction in a schema \"direction, distance\" ");
        answer = Console.ReadLine();
        string direction = answer.Split(",")[0];
        string distance = answer.Split(",")[1];
        await call.RequestStream.WriteAsync(new PositionRequest()
        {
            Direction = direction,
            Distance = int.Parse(distance)
        });
        if (await call.ResponseStream.MoveNext(CancellationToken.None))
        {
            Console.WriteLine(call.ResponseStream.Current);
        }

    }
}