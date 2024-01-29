using System.Runtime.Serialization;

namespace Notifications.Application.Common.Exceptions;

public class BadRequestException : Exception
{
    /// <summary>
    /// 
    /// </summary>
    public BadRequestException()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public BadRequestException(string message) : base(message)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    /// <returns></returns>
    public BadRequestException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    protected BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}