using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Domain;

namespace Vezeeta.Core.Specifications
{
    public interface ISpecification<T> where T : BaseDomain
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> Includes { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }
    }
}
