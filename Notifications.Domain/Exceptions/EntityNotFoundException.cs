namespace Notifications.Domain.Exceptions;

public class EntityNotFoundException : ApplicationException
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="key"></param>
    public EntityNotFoundException(string entity, object key)
        : base($"Entity \"{entity}\" ({key}) was not found.")
    {
        
    }
}