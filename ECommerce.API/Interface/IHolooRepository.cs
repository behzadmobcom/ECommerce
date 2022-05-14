namespace API.Interface;

public interface IHolooRepository<T> : IDisposable
{
    Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken);
}