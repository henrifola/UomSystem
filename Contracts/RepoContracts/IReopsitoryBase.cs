using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Contracts.RepoContracts
{
    public interface IRepositoryBase<T>
        {
            //CRUD
            void Create(T e);
            Task<T> Get(string id);
            ICollection<T> GetAll();
            void update(T e);
            void delete(T e);
        }
}