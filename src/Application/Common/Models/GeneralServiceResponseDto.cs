namespace Application.Common.Models;

public class GeneralServiceResponseDto<T>
{
    public bool IsSucceed { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public List<string> Errors { get; set; } = new();

    public static GeneralServiceResponseDto<T> Success(T data, string message)
    {
        return new GeneralServiceResponseDto<T>
        {
            IsSucceed = true,
            StatusCode = 200,
            Message = message,
            Data = data
        };
    }

    public static GeneralServiceResponseDto<T> Failure(string message, int statusCode = 400, List<string> errors = null)
    {
        return new GeneralServiceResponseDto<T>
        {
            IsSucceed = false,
            StatusCode = statusCode,
            Message = message,
            Errors = errors ?? new List<string>()
        };
    }
}

