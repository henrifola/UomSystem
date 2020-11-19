using System;
using System.Collections.Generic;
using Data;
using Data.Models;


namespace DatabaseInitializer
{
    public class DbInitializer
    {
        public static void Initialize(RepositoryContext context)
        {
            context.Database.EnsureDeleted();

            context.Database.EnsureCreated();
            

            context.UnitOfMeasures.Add(new UnitOfMeasure(id: "1Pkg", annotation: "1/kg", name: "per kilogram"));
            context.UnitOfMeasures.Add(new UnitOfMeasure(id: "km", annotation: "km", name: "kilometer"));

            context.SaveChanges();
        }
    }
}

/*
 <UnitOfMeasure id="1Pkg" annotation="1/kg">
<Name>per kilogram</Name>
<QuantityType>per mass</QuantityType>
<DimensionalClass>1/M</DimensionalClass>
<SameUnit uom="1/kg" namingSystem="RP66"/>
<CatalogName>POSC</CatalogName>
<CatalogSymbol isExplicit="true">1/kg</CatalogSymbol>
<BaseUnit/>
 */