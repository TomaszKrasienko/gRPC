namespace gRPC.BidirectionalStreaming.Server.Services;

public interface IMovementService
{
    Tuple<string, string> GetCurrentPosition(string direction, int distance);
}