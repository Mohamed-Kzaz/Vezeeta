using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Domain;
using Vezeeta.Core.Specifications;

namespace Vezeeta.Repository
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseDomain
    {
        // This Method Will Build Our Query
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            // Comments For Just Clarification
            var query = inputQuery; 

            if (spec.Criteria is not null) 
                query = query.Where(spec.Criteria);


            if (spec.IsPaginationEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);


            // We Used Aggregate To Combine Query
            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            return query;
        }
    }
}
