using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.RepoContracts
{
    public interface IRepositoryBase<T>
        {
            //CRUD
            void Create(T e);
            Task<T> Get(string id);
            Task<IList<T>> GetAll();
            void update(T e);
            void delete(T e);
        }
}