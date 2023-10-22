namespace gRPC.ClientStreaming.Server.Services;

public interface IExamAnalyzer
{
    bool VerifyResult(int taskNumber, bool result);
    double GetSummary();
}