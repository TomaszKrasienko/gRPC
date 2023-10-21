using gRPC.ServerStreaming.Server.Models;

namespace gRPC.ServerStreaming.Server.Services;

public class StockService : IStocksService
{
    public Overview GetStocksOverview()
    {
        var random = new Random();
        
        return new Overview(DateTimeOffset.Now, new List<Stock>()
        {
            new Stock(random.NextDouble(), "Google"),
            new Stock(random.NextDouble(), "Microsoft"),
            new Stock(random.NextDouble(), "Apple")
        });
    }
}