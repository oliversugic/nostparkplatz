namespace MongoDBDemoApp.Core.Util;

public interface IDateTimeProvider
{
    DateTime Now { get; }
}