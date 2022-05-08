using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace API.Interface
{
    public interface IHolooRepository<T> : IDisposable
    {
        Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken);
        
    }
}
