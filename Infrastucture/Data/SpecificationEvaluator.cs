using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Data
{
    class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec) {
            var query = inputQuery;

            if (spec.Criteria != null) {
                query.Where(spec.Criteria);
            }

            query = spec.Includes.Aggregate(query,(current,include) => current.Include(include));
            return query;
        }
    }
}
