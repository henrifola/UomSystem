using Contracts.RepoContracts;

namespace Contracts.UnitOfMeasureContracts
{
    public interface IEngineeringUnitsWrapper
    {
   
        IQuantityRepo QuantityType { get;  }
        IDimensionalRepo DimensionalClass { get;  }
        ICustomaryUnitRepo CustomaryUnit { get;  }
        IUnitConversion UnitConverter { get;  }
        IUnitOfMeasureRepo UnitOfMeasure { get;  }
        

    }
}