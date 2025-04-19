using DomainLayer.Models;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        // Where Condition
        Expression<Func<TEntity, bool>>? Criteria { get; }
        // Include Related Data
        List<Expression<Func<TEntity, object>>> Includes { get; }
    }
}
