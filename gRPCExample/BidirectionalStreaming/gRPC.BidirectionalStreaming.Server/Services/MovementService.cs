namespace gRPC.BidirectionalStreaming.Server.Services;

public class MovementService : IMovementService
{
    private int currentXPosition = 0;
    private int currentYPosition = 0;
    
    public Tuple<string, string> GetCurrentPosition(string direction, int distance)
    {
        HandleXPosition(direction, distance);
        HandleYPosition(direction, distance);
        return new Tuple<string, string>(currentXPosition.ToString(), currentYPosition.ToString());
    }

    private void HandleXPosition(string direction, int distance)
        => currentXPosition = direction switch
        {
            "right" => currentXPosition + distance,
            "left" => currentXPosition - distance,
            _ => currentXPosition
        };

    private void HandleYPosition(string direction, int distance)
        => currentYPosition = direction switch
        {
            "up" => currentYPosition + distance,
            "down" => currentYPosition - distance,
            _ => currentYPosition
        };
}