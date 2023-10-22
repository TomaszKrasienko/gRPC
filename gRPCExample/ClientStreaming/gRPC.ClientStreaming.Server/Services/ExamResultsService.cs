using Grpc.Core;

namespace gRPC.ClientStreaming.Server.Services;

public class ExamResultsService : Server.ExamResultsService.ExamResultsServiceBase
{
    private readonly IExamAnalyzer _examAnalyzer;

    public ExamResultsService(IExamAnalyzer examAnalyzer)
    {
        _examAnalyzer = examAnalyzer;
    }

    public override async Task<ExamTaskResultsResponse> SendResults(IAsyncStreamReader<ExamTaskAnswerRequest> requestStream, ServerCallContext context)
    {
        List<AnswersResult> answers = new List<AnswersResult>(); 
        while (await requestStream.MoveNext())
        {
            var reqStream = requestStream.Current;
            var res = _examAnalyzer.VerifyResult(reqStream.TaskNumber, reqStream.Result);
            answers.Add(new AnswersResult()
            {
                TaskNumber = reqStream.TaskNumber,
                IsValid = res,
                ProvidedResult = reqStream.Result
            });
        }

        var tmp = _examAnalyzer.GetSummary();

        return new ExamTaskResultsResponse()
        {
            SummaryResult = _examAnalyzer.GetSummary(),
            Details =
            {
                answers
            }
        };
    }
    
    
}