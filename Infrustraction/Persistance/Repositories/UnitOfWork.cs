using DomainLayer.Contracts;
using DomainLayer.Models;
using Persistance.Data;
using Persistance.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private Dictionary<Type, object> repositories =[];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            // Check if The repository is already created
            if(repositories.ContainsKey(typeof(TEntity)))
                return (IGenericRepository<TEntity, TKey>)repositories[typeof(TEntity)];
            // Create the repository
            var Repository = new GenericRepository<TEntity, TKey>(_dbContext);
            repositories.Add(typeof(TEntity), Repository);
            return Repository;
        }

        public async Task<int> SaveChangesAsync()
        {
            // Save changes to the database
            return await _dbContext.SaveChangesAsync();
        }
    }
}
