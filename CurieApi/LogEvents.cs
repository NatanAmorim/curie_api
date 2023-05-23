namespace sgd_cms.Logging;

public class LogEvents
{
  public static readonly EventId Read = new(1000, "Reading");
  public static readonly EventId Created = new(1001, "Creating");
  public static readonly EventId Updating = new(1002, "Updating");
  public static readonly EventId Deleting = new(1003, "Deleting");

  public static readonly EventId ReadSuccessful = new(2000, "ReadSuccessful");
  public static readonly EventId CreateSuccessful = new(2001, "CreateSuccessful");
  public static readonly EventId UpdateSuccessful = new(2002, "UpdateSuccessful");
  public static readonly EventId DeleteSuccessful = new(2003, "DeleteSuccessful");
  public static readonly EventId Ok = new(2004, "Ok");

  public static readonly EventId ReadFailed = new(4000, "ReadFailed");
  public static readonly EventId CreateFailed = new(4001, "CreateFailed");
  public static readonly EventId UpdateFailed = new(4002, "UpdateFailed");
  public static readonly EventId DeleteFailed = new(4003, "DeleteFailed");
  public static readonly EventId Conflict = new(4004, "Conflict");
  public static readonly EventId AuthFailed = new(4005, "AuthFailed");
  public static readonly EventId InvalidInput = new(4006, "InvalidInput");
  public static readonly EventId Forbidden = new(4007, "Forbidden");
  public static readonly EventId PaymentRequired = new(4008, "PaymentRequired");
  public static readonly EventId PayloadTooLarge = new(4009, "PayloadTooLarge");

  public static readonly EventId Error = new(5000, "Error");
  public static readonly EventId Timeout = new(5001, "Timeout");
  public static readonly EventId InsufficientStorage = new(5002, "InsufficientStorage");
}