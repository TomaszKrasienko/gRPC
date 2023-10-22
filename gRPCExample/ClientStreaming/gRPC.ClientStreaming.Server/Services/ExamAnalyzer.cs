namespace gRPC.ClientStreaming.Server.Services;

public class ExamAnalyzer : IExamAnalyzer
{
    private int validAnswers = 0;
    private int tasksNumber = 0;
    public bool VerifyResult(int taskNumber, bool result)
    {
        tasksNumber++;
        if (taskNumber % 2 == 0 && result)
        {
            validAnswers++;
            return true;
        }
        if (taskNumber % 2 == 1 && !result)
        {
            validAnswers++;
            return true;
        }
        return false;
    }

    public double GetSummary()
    {
        var fraction = (double)validAnswers / (double)tasksNumber; 
        return fraction * 100;
    }
}