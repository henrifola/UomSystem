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

            insert_xml_data(context);
            
            
            Console.WriteLine("reading data");
            Console.WriteLine(context.UnitOfMeasures.Find("yr(100k)" ).Name);
            
            var dimClass=context.DimenensionalClasses.Find("T");

            Console.WriteLine("Units inside dim class "+ dimClass.Notation);
            foreach (var unit in dimClass.Units)
            {
                Console.Write(unit.Name+ ", ");
            }
            
        }

        private static void insert_xml_data(RepositoryContext context)
        {
            XmlHandler xmlHandler = new XmlHandler();
            var unitOfMeasures = xmlHandler.CreateUoms();
            context.UnitOfMeasures.AddRange(unitOfMeasures);
            

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