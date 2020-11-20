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
            
            // Reading Tests
            Console.WriteLine("reading data");
            Console.WriteLine();
            
            var dimClass=context.DimenensionalClasses.Find("T");

            Console.WriteLine("Units inside dim class "+ dimClass.Notation);
            foreach (var unit in dimClass.Units)
            {
                Console.Write(unit.Name+ ", ");
            }

            var u = context.UnitOfMeasures.Find("yr(100k)"); //gir object
            var c= context.CustomaryUnits.Find("yr(100k)"); //gir object
            var b = context.CustomaryUnits.Find("J"); //gir null
            var b2 = context.UnitOfMeasures.Find("J"); //gir object
            Console.WriteLine(u.Id);
            Console.WriteLine(c.Id);
            

        }

        private static void insert_xml_data(RepositoryContext context)
        {
            Console.WriteLine("Parsing xml..");
            XmlHandler xmlHandler = new XmlHandler();
            Console.WriteLine("writing to the database..");
            context.UnitOfMeasures.AddRange(xmlHandler.unitOfMeasures);
            context.CustomaryUnits.AddRange(xmlHandler.customaryUnits);
            Console.WriteLine("Done.");

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