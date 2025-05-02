namespace Application.ResultPattern;
public class Result
{
    public bool Success { get; set; }
    public string Message { get; set; } = default!;
    public static Result SuccessResult(string message ) =>
        new()
        { Success = true, Message = message };

    public static Result FailureResult(string message) =>
        new()
        { Success = false, Message = message };
}