using Grpc.Core;

namespace gRPC.BidirectionalStreaming.Server.Services;

public class PositionService : Server.PositionService.PositionServiceBase
{
    private readonly IMovementService _movementService;
    
    public PositionService(IMovementService movementService)
    {
        _movementService = movementService;
    }

    public override async Task ShowResults(IAsyncStreamReader<PositionRequest> requestStream, IServerStreamWriter<PositionResponse> responseStream, ServerCallContext context)
    {
        while (await requestStream.MoveNext())
        {
            var req = requestStream.Current;
            var positions = _movementService.GetCurrentPosition(req.Direction, req.Distance);
            await responseStream.WriteAsync(new PositionResponse()
            {
                XPosition = positions.Item1,
                YPosition = positions.Item2
            });
        }
    }
}