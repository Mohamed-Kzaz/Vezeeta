using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Domain;
using Vezeeta.Core.Repository;

namespace Vezeeta.Core.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<TEntity>? Repository<TEntity>() where TEntity : BaseDomain;
        Task<int> Complete();
    }
}
