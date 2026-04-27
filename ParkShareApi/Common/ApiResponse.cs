namespace ParkShareApi.Common;

public class ApiResponse<T>
{
    public int StatusCode { get; set; }
    public string StatusMessage { get; set; } = string.Empty;
    public T? Data { get; set; }

    public static ApiResponse<T> Success(T data, string message = "Success")
    {
        return new ApiResponse<T>
        {
            StatusCode = 200,
            StatusMessage = message,
            Data = data
        };
    }

    public static ApiResponse<T> Created(T data, string message = "Created successfully")
    {
        return new ApiResponse<T>
        {
            StatusCode = 201,
            StatusMessage = message,
            Data = data
        };
    }

    public static ApiResponse<T> Fail(int statusCode, string message)
    {
        return new ApiResponse<T>
        {
            StatusCode = statusCode,
            StatusMessage = message,
            Data = default
        };
    }
}
