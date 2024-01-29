namespace Notifications.Application.Common.Exceptions;

public class ApplicationException : Exception
{
    /// <summary>
    /// 
    /// </summary>
    public ApplicationException()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="errorKey"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public ApplicationException(string errorKey, string message) : base(message)
    {
        ErrorKey = errorKey;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public ApplicationException(string message) : base(message)
    {
    }

    public string ErrorKey { get; set; } = null!;
}