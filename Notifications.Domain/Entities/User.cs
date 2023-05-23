namespace Notifications.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string CountryCode { get; set; }
    public int? Gender { get; set; }
    public int? AccountType { get; set; }
    public int? SubscriptionType { get; set; }
    public string AvatarUrl { get; set; }
    public string PhoneNumber { get; set; }
    
    #region [REFERENCE PROPERTIES]
    public virtual UserFirebaseToken UserFirebaseToken { get; set; }
    public ICollection<Notification> Notifications { get; set; }
    public ICollection<NotificationTypeUser> NotificationTypeUsers { get; set; }
    #endregion [REFERENCE PROPERTIES]
}