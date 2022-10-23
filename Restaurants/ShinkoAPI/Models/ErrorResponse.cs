namespace ShinkoAPI.Models;

/// <summary>
/// Represents response errors that are sent if something went wrong.
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// The error message that explains the reason for the exception
    /// </summary>
    public string Message { get; set; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorResponse"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public ErrorResponse(string message)
    {
        Message = message;
    }
}