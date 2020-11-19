using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Data.Models;

namespace DatabaseInitializer
{
    // TODO parsing diagram
    public class XmlHandler  : IXmlHandler
    {
        private XElement data;

        public XmlHandler()
        {
            
        }

        public XElement ReadFile()
        {
            var filename = "..\\DatabaseInitializer\\poscUnits22.xml";
            var currentDirectory = Directory.GetCurrentDirectory();
            var filePath = Path.Combine(currentDirectory, filename);
            return XElement.Load(filePath);

        }

        public List<UnitOfMeasure> CreateUoms()
        {
            var xml = ReadFile();

            var units = from item in xml.Descendants("UnitOfMeasure") select item;
            Console.WriteLine(units.FirstOrDefault());
            Console.WriteLine(units.Last());
            
            List<UnitOfMeasure> unitOfMeasures = new List<UnitOfMeasure>();

            Dictionary<string,DimensionalClass> dimensionalClasses = new Dictionary<string, DimensionalClass>();
            
            //dimensionalClasses.Values.ToList();
            
            foreach (var unit in units)
            {
                UnitOfMeasure unitOfMeasure = new UnitOfMeasure();
                string id = (string) unit.Attribute("id");
                unitOfMeasure.Id = id;
                unitOfMeasure.Annotation = (string) unit.Attribute("annotation");
                unitOfMeasure.Name = unit.Descendants("Name").First().Value;

                var sameUnits = unit.Descendants("SameUnit");
    
                /*
                foreach (var sameUnit in sameUnits)
                {
                    unitOfMeasure.SameUnits=new List<SameUnit>();
                    var s = sameUnit.Attribute("uom");
                    if (s!=null) unitOfMeasure.SameUnits.Add(new SameUnit(s.Value));
                    else
                    {
                        Console.WriteLine("error, no attribute uom for same unit");
                    }
                }
                */

                var dim = unit.Descendants("DimensionalClass").FirstOrDefault();
                
                if (dim == null)
                {
                    Console.WriteLine("Unit with no dim class:"); //there is 1 unit without dimensional class: "gu"
                    Console.WriteLine(unit);
                    continue;
                }

                string dimensionalClassId = dim.Value;

                
                
                if(!dimensionalClasses.ContainsKey(dimensionalClassId))
                {
                    dimensionalClasses.Add(dimensionalClassId,new DimensionalClass(dimensionalClassId) );
                }
                
                dimensionalClasses[dimensionalClassId].Units.Add(unitOfMeasure);

                unitOfMeasure.DimensionClassId = dimensionalClassId;
                
                unitOfMeasures.Add(unitOfMeasure);
            }

            foreach (var unit in unitOfMeasures)
            {
                unit.DimensionalClass = dimensionalClasses[unit.DimensionClassId];
            }

            return unitOfMeasures;

            //public UnitOfMeasure(string id, string annotation, string name, DimensionalClass dimensionalClass,
            //ICollection<QuantityType> quantityTypes, List<SameUnit> sameUnits)

            //foreach (var v in test) { Console.WriteLine(v); }
        }
        
        

    }
}