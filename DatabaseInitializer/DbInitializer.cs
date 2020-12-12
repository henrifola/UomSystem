using System;
using System.Data;
using Contracts.InitializerContracts;
using Data;
namespace DatabaseInitializer
{
    public class DbInitializer : IDbInitializer
    {
        public static void Initialize(RepositoryContext context)
        {
            if (context == null) throw new DataException();
            context.Database.EnsureDeleted();
            var created= context.Database.EnsureCreated();
            if (!created) {
                throw new NotImplementedException();
            } 
            
            insert_xml_data(context);
            read_testing(context);
        }

        private static void insert_xml_data(RepositoryContext context)
        {
            Console.WriteLine("Parsing xml..");
            var xmlHandler = new XmlHandler();
            Console.WriteLine("writing to the database..");
            context.UnitOfMeasures.AddRange(xmlHandler.UnitOfMeasures);
            context.CustomaryUnits.AddRange(xmlHandler.CustomaryUnits);
            
            Console.WriteLine("Done.");

            context.SaveChanges();
        }
        
        // Denne biten kan være i et eget prosjekt, et eget testing prosjekt
        private static void read_testing(RepositoryContext context)
        {
            Console.WriteLine("reading data");
            Console.WriteLine();
            
            var dimClass=context.DimensionalClasses.Find("T");

            Console.WriteLine("Units inside dim class "+ dimClass.Notation);
            foreach (var unit in dimClass.Units)
            {
                Console.Write(unit.Name+ ", ");
            }
            
            // Quantity types 
            var u = context.UnitOfMeasures.Find("uS"); //gir object
            var c= context.CustomaryUnits.Find("uS"); //gir object
            var b = context.CustomaryUnits.Find("1Pm"); //gir null
            var b2 = context.UnitOfMeasures.Find("1Pm"); //gir object
            Console.WriteLine(u.Id);
            Console.WriteLine(c.Id);

            Console.WriteLine(b2.Annotation + " contains quantity types:");
            foreach (var uomqt in b2.UnitOfMeasureQuantityTypes)
            {
                Console.WriteLine(uomqt.QuantityType.Notation);
            }

        }
    }
}

