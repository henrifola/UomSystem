using Contracts.RepoContracts;
using Data;
using Data.Models;
using UomRepository.Common;

namespace EngineeringUnitscore.Repos
{
    //Class to access the uom repo
    public class UnitOfMeasureRepo: RepositoryBase<UnitOfMeasure>, IUnitOfMeasureRepo

    //public class CustomaryUnitRepo : RepositoryBase<CustomaryUnit>, ICustomaryUnitRepo
    {
        public UnitOfMeasureRepo(RepositoryContext context) : base(context)
        {
        }
    }
}