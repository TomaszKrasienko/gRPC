namespace gRPC.Unary.Server.Models;

public record TodoTask(int Id, string Content, DateTimeOffset EndTime);