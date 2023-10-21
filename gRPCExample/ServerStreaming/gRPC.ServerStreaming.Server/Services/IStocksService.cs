using gRPC.ServerStreaming.Server.Models;

namespace gRPC.ServerStreaming.Server.Services;

public interface IStocksService
{
    Overview GetStocksOverview();
}