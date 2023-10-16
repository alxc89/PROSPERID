namespace PROSPERID.Application.Services.Shared;

public class ServiceResponse<T>
{
    public string Message { get; set; } = string.Empty;
    public int Status { get; set; } = 400;
    public bool IsSuccess => Status is 200 and <= 299;
    public T? Data { get; set; }

    protected ServiceResponse() { }

    public ServiceResponse(string message, int status)
    {
        Message = message;
        Status = status;
    }

    public ServiceResponse(string message, int status, T data)
    {
        Message = message;
        Status = status;
        Data = data;
    }
}
