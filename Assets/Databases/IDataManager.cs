
public interface IDatabase : IReadDb, IWriteDb {}

public interface IReadDb
{
    public T LoadFromJson<T>(string directory);
}
public interface IWriteDb
{
    public void Save<T>(T data, string directory);
}