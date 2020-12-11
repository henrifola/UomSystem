using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.RepoContracts;
using Contracts.UnitOfMeasureContracts;
using Data;

namespace UomRepository.Common
{
    public abstract class RepositoryBase<T> : DataAcces, IRepositoryBase<T> where T : class
    {
        protected RepositoryBase(RepositoryContext context) : base(context){}


        public void Create(T e)
        {
            throw new System.NotImplementedException();
        }

        public async Task<T> Get(string id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public Task<IList<T>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void update(T e)
        {
            throw new System.NotImplementedException();
        }

        public void delete(T e)
        {
            throw new System.NotImplementedException();
        }
    }
}