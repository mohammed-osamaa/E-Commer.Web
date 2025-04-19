using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    internal static class SpecificationCreation
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> query, ISpecifications<TEntity, TKey> specifications) where TEntity : BaseEntity<TKey>
        {
            if (specifications.Criteria != null)
                query = query.Where(specifications.Criteria);

            if(specifications.OrderBy != null)
                query = query.OrderBy(specifications.OrderBy);
            if (specifications.OrderByDescending != null)
                query = query.OrderByDescending(specifications.OrderByDescending);
            //foreach (var include in specifications.Includes)
            //    query =query.Include(include);
            if (specifications.Includes != null && specifications.Includes.Count > 0)
                query = specifications.Includes.Aggregate(query ,(CurrentQuery , Include) => CurrentQuery.Include(Include));

            if (specifications.IsPagingEnabled)
                query = query.Skip(specifications.Skip).Take(specifications.Take);

            return query;
        }

    }
}
