using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];

        public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
            //Get Type Name
            var typeName = typeof(TEntity).Name;
            // Dic<string , object> ==> string [Name of Type] -- object From  GenericRepository
            //if(_repositories.ContainsKey(typeName))
            //    return (IGenericRepository<TEntity ,  Tkey>) _repositories[typeName];

            if (_repositories.TryGetValue(typeName, out object? value))
                return (IGenericRepository<TEntity, Tkey>) value;
            else
            {
                //Create Object
                var Repo = new GenericRepository<TEntity, Tkey>(_dbContext);
                //Store in Dic
                _repositories.Add(typeName, Repo);
                //Return object
                return Repo;
            }
        }

        public async Task<int> SaveChange() => await _dbContext.SaveChangesAsync();


    }
}
