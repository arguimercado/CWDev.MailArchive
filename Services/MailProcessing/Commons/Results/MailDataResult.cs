namespace MailProcessing.Commons.Results;

public class MailDataResult(string? emlFilePath, string headers, string summary)
{
    public string? EmlFilePath { get; init; } = emlFilePath;
    public string Headers { get; init; } = headers;
    public string Summary { get; init; } = summary;
}