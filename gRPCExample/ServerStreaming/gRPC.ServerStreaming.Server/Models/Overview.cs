namespace gRPC.ServerStreaming.Server.Models;

public record Overview(DateTimeOffset ReleaseDate, List<Stock> Stocks);