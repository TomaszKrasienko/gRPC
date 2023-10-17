namespace gRPC.Unary.Server.Helpers;

public interface IClock
{
    DateTimeOffset Current();
}