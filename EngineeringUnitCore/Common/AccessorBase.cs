using System.Collections.Generic;
using System.Linq;
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
            Context.Set<T>().AddAsync(e);
        }

        public async Task<T> Get(string id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public ICollection<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        public void update(T e)
        {
             Context.Set<T>().Update(e);
        }

        public void delete(T e)
        {
            Context.Set<T>().Remove(e);
        }
    }
}