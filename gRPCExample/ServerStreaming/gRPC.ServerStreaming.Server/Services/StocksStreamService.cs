using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using gRPC.ServerStreaming.Server.Models;

namespace gRPC.ServerStreaming.Server.Services;

public class StocksStreamService : StocksServerStreaming.StocksServerStreamingBase
{
    private readonly ILogger<StocksStreamService> _logger;
    private readonly IStocksService _stocksService;

    public StocksStreamService(ILogger<StocksStreamService> logger, IStocksService stocksService)
    {
        _logger = logger;
        _stocksService = stocksService;
    }

    public override async Task SubscribeStocks(Empty request, IServerStreamWriter<OverviewResponse> responseStream, ServerCallContext context)
    {    
        _logger.LogInformation("Subscribe starts ...");
        while (true)
        {
            await Task.Delay(TimeSpan.FromSeconds(10));
            var stocksOverview = _stocksService.GetStocksOverview();
            var overViews = Map(stocksOverview);
            foreach (var item in overViews)
            {
                _logger.LogInformation($"Getting stock {item.Stock.StockName}");
                await responseStream.WriteAsync(item);
            }
            // await responseStream.WriteAsync(new StockResponse()
            // {
            //     Value = 1,
            //     StockName = "test1",
            //     StockShortcut = "test2"
            // });
        }
    }

    private List<OverviewResponse> Map(Overview overview)
        => overview.Stocks.Select(x => new OverviewResponse()
        {
            Stock = new StockResponse()
            {
                Value = x.Value,
                StockName = x.Name
            },
            ReleaseDate = overview.ReleaseDate.ToTimestamp()
        }).ToList();
    
    
    // public override async Task SubscribeStocks(Empty request, IServerStreamWriter<StockResponse> responseStream, ServerCallContext context)
    // {
    //     while (true)
    //     {
    //         await Task.Delay(TimeSpan.FromSeconds(10));
    //         var stocksOverview = _stocksService.GetStocksOverview();
    //         await responseStream.WriteAsync(new StockResponse()
    //         {
    //             Value = 1,
    //             StockName = "test1",
    //             StockShortcut = "test2"
    //         });
    //     }
    // }
}