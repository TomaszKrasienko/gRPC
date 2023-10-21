using Grpc.Core;

namespace gRPC.ClientStreaming.Server.Services;

public class ExamResultsService : Server.ExamResultsService.ExamResultsServiceBase
{
    public override async Task<ExamTaskResultsResponse> SendResults(IAsyncStreamReader<ExamTaskAnswerRequest> requestStream, ServerCallContext context)
    {
        var answers = new List<ExamTaskAnswerRequest>();
        while (await requestStream.MoveNext())
        {
            answers.Add(requestStream.Current);
        }

        return new ExamTaskResultsResponse()
        {
            SummaryResult = 1,
            Details =
            {
                answers.Select(x =>
                    new AnswersResult()
                    {
                        TaskNumber = x.TaskNumber,
                        IsValid = true,
                        ProvidedResult = x.Result
                    }).ToList()
            }
        };
    }
    
    
}