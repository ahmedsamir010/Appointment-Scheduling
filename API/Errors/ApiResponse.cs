namespace API.Errors;
public class ApiResponse(int statusCode, string? message = null)
{
    public int StatusCode { get; set; } = statusCode;
    public string Message { get; set; } = message ?? GetDefaultMessageForStatusCode(statusCode);

    private static string GetDefaultMessageForStatusCode(int statusCode)
    {
        return statusCode switch
        {
            200 => "Success - The request was successful",
            400 => "Bad Request - The request was invalid",
            401 => "Unauthorized - Authentication required",
            403 => "Forbidden - Access denied",
            404 => "Not Found - Resource not found",
            405 => "Not Allowed - Method not allowed",
            500 => "Server Error - Something went wrong on the server",
            _ => "Error - An unexpected error occurred"
        };
    }
    public override string ToString() => $"{StatusCode} - {Message}";
}