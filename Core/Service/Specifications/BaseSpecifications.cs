using DomainLayer.Contracts;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    internal abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity , TKey> where TEntity : BaseEntity<TKey>
    {
        public BaseSpecifications(Expression<Func<TEntity, bool>>? criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<TEntity, bool>>? Criteria { get; }
        public List<Expression<Func<TEntity, object>>> Includes { get; } = new List<Expression<Func<TEntity, object>>>();

        public void AddIncludes(Expression<Func<TEntity, object>> include)
        { 
            Includes.Add(include);
        }
    }
}
