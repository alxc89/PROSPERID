namespace PROSPERID.Application.Services.Shared;

public class ServiceResponse<T>
{
    /// <summary>
    /// Mensagem de retorno.
    /// </summary>
    public string Message { get; set; } = string.Empty;
    
    /// <summary>
    /// Status Code.
    /// </summary>
    public int Status { get; set; } = 400;

    /// <summary>
    /// Indica se a operação foi sucesso.
    /// </summary>
    /// <example>True</example>
    public bool IsSuccess => Status is 200 and <= 299;
    
    /// <summary>
    /// Objeto do retorno.
    /// </summary>
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
