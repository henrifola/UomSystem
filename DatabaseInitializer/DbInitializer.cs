using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Contracts.InitializerContracts;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;


namespace DatabaseInitializer
{
    
    public class DbInitializer : IDbInitializer
    {
        private RepositoryContext _context;
        public static void Initialize(RepositoryContext context)
        {
            

            if (context == null)
            {
                throw new DataException();
            }

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
            XmlHandler xmlHandler = new XmlHandler();
            Console.WriteLine("writing to the database..");
            //context.QuantityTypes.AddRange(xmlHandler.QunatiyTypes);
            context.UnitOfMeasures.AddRange(xmlHandler.UnitOfMeasures);
            context.CustomaryUnits.AddRange(xmlHandler.CustomaryUnits);
            //context.UnitOfMeasureQuantityTypes.AddRange(xmlHandler.UomQunatiyTypes);
            
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
            
            
            // Units of measure quantity types
            /*
            var uomQuantityTypes = context.UnitOfMeasureQuantityTypes.Select(x=>x);

            foreach (var x in uomQuantityTypes)
            {
                Console.WriteLine(x.QuantityTypeId+":"+ x.UnitOfMeasureId);
                Console.WriteLine(x.UnitOfMeasure.Name+":"+x.UnitOfMeasure.DimensionalClass);
                Console.WriteLine();
                
            }
            */
            
            // Quantity types

            /*
            var qts = from q in context.QuantityTypes
                where q.UnitOfMeasureQuantityTypes.Count > 1
                select q;

            foreach (var qt in qts)
            {
                Console.Write("quatity type: "+qt.Notation+"\nUoms: ");
                foreach (var uomqt in qt.UnitOfMeasureQuantityTypes)
                {
                    Console.Write(uomqt.UnitOfMeasure.Name+ ", ");
                }
                Console.WriteLine("\n");
            }
            */

        }
    }
}

