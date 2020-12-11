using Contracts.RepoContracts;
using Data;
using Data.Models;
using UomRepository.Common;

namespace EngineeringUnitscore.Repos
{
    //Class to access customary unit repo
    public class CustomaryUnitRepo : RepositoryBase<CustomaryUnit>, ICustomaryUnitRepo
    {
        public CustomaryUnitRepo(RepositoryContext context) : base(context)
        {
        }
    }
}