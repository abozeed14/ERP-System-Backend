public interface IUnitOfWork
{
    Task<int> CompleteAsync();
    void Dispose();
}