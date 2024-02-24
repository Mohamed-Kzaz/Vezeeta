using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Domain;
using Vezeeta.Core.Specifications;

namespace Vezeeta.Core.Repository
{
    public interface IGenericRepository<T> where T : BaseDomain
    {
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);
        Task<T> GetEntityWithSpecAsync(ISpecification<T> spec);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByStringAsync(string value);
        Task<T> GetByIdAsync(int id);
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
