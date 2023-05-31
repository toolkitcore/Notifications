namespace Notifications.Application.Common.Exceptions;

public static class ErrorCode
{
    public const string NameMinLength5MaxLength50 = "NAME_MIN_LENGTH_5_MAX_LENGTH_50";
    public const string CodeMinLength5MaxLength50 = "CODE_MIN_LENGTH_5_MAX_LENGTH_50";
    public const string CodeInvalid = "CODE_INVALID";
    public const string SubjectMinLength5MaxLength50 = "SUBJECT_MIN_LENGTH_5_MAX_LENGTH_50";
    public const string NameExisted = "NAME_EXISTED";
    public const string CodeExisted = "CODE_EXISTED";
    public const string ApplicationNotFound = "APPLICATION_NOT_FOUND";
    public const string NotificationGroupNotFound = "NOTIFICATION_GROUP_NOT_FOUND";
    public const string NotificationTypeChannelNotFound = "NOTIFICATION_TYPE_CHANNEL_NOT_FOUND";
    public const string NotificationTypeNotFound = "NOTIFICATION_TYPE_NOT_FOUND";
    public const string FieldIsRequired = "FIELD_IS_REQUIRED";
    public const string ChannelInvalid = "CHANNEL_INVALID";
    public const string NotificationTypeStatusInvalid = "NOTIFICATION_TYPE_STATUS_INVALID";
    public const string UserFirebaseTokenNotFound = "USER_FIREBASE_TOKEN_NOT_FOUND";
    public const string NotificationTypeStatusNotFound = "NOTIFICATIONTYPE_STATUS_NOT_FOUND";
    public const string NameIsRequired = "NAME_IS_REQUIRED";
    public const string NotificationTypeNameMinLength5MaxLength50LettersSpace = "NAME_MIN_LENGTH_5_MAX_LENGTH_50_LETTERS_SPACE";
    public const string ChannelLanguageExisted = "CHANNEL_LANGUAGE_EXISTED";
    public const string UserNotFound = "USER_NOT_FOUND";
    public const string UserIdsOrDestinationsIsRequired = "USERIDS_OR_DESTINATIONS_IS_REQUIRED";
    public const string DestinationsMustBeEmailOrPhoneNumber = "DESTINATIONS_MUST_BE_EMAIL_OR_PHONENUMBER";

    public const string NotificationTypeStatusNotSupport = "NOTIFICATION_TYPE_STATUS_NOT_SUPPORT";
    public const string SubjectIsRequired = "SUBJECT_IS_REQUIRED";
    public const string ContentIsRequired = "CONTENT_IS_REQUIRED";
    public const string LanguageIsRequired = "LANGUAGE_IS_REQUIRED";
    public const string NotificationTypeIdIsRequired = "NOTIFICATION_TYPE_ID_IS_REQUIRED";
    public const string LanguageCodeMinLength2MaxLength10Letters = "LANGUAGECODE_MIN_LENGTH_2_MAX_LENGTH_10_LETTERS";
    public const string NotificationNotFound = "NOTIFICATION_NOT_FOUND";
    public const string NoNotificationMaskRead = "NO_NOTIFICATION_MASK_READ";
    public const string NotificationNotFoundOrStatusNotAvaiable = "NOTIFICATION_NOT_FOUND_OR_STATUS_NOT_AVAIABLE";
    public const string NotificationExistedWithNotificationTypeId = "NOTIFICATION_EXISTED_WITH_NOTIFICATIONTYPEID";
    public const string NotificationTypeUserExistedWithNotificationTypeId = "NOTIFICATION_TYPE_USER_EXISTED_WITH_NOTIFICATIONTYPEID";
    public const string ApplicationCodeInvalid = "APPLICATION_CODE_INVALID";
    public const string NotificationGroupInvalid = "NOTIFICATION_GROUP_INVALID";
    public const string ApplicationCodeRequired = "APPLICATION_CODE_REQUIRED";
    public const string IsRequired = "IS_REQUIRED";
    public const string InvalidEmail = "INVALID_EMAIL";
    public const string TokenNotFoundInVerificationUrl = "TOKEN_NOT_FOUND_IN_VERIFICATION_URL";
    public const string UrlInvalid = "URL_INVALID";
    public const string TokenInvalid = "TOKEN_INVALID";
    public const string TokenExpired = "TOKEN_EXPIRED";
}