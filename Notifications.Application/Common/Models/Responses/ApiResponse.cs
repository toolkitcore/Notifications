using System.Net;

namespace Notifications.Application.Common.Models.Responses;

public class ApiResponse : ApiResponseBase
{
    /// <summary>
    /// 
    /// </summary>
    public ApiResponse()
    {
        StatusCode = HttpStatusCode.OK;
    }
}

public class ApiResponse<T> : ApiResponseBase
{
    public T Data { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public ApiResponse()
    {
        Data = default;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    public ApiResponse(T data)
    {
        Data = data;
        StatusCode = HttpStatusCode.OK;
    }
}