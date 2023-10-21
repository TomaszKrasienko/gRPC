using System.Linq.Expressions;
using Grpc.Core;
using Grpc.Net.Client;
using gRPC.ServerStreaming.Server;

Console.WriteLine("Please type \"start\" to begin stream");
string input = Console.ReadLine();

if (input is "start")
{
    using var channel = GrpcChannel.ForAddress("http://localhost:5000");
    var client = new StocksServerStreaming.StocksServerStreamingClient(channel);
    using var reply = client.SubscribeStocks(new Empty());
    while (await reply.ResponseStream.MoveNext())
    {
        var overview = reply.ResponseStream.Current;
        Console.WriteLine(
            $"Release date: {overview.ReleaseDate}\nStock name: {overview.Stock.StockName}\nStock value:{overview.Stock.Value}");
    }
}