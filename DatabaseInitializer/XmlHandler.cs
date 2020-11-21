using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Data.Models;

namespace DatabaseInitializer
{
    // TODO parsing diagram
    public class XmlHandler 
    {
        private XElement xml;
        public List<UnitOfMeasure> unitOfMeasures  {get;}
        public List<CustomaryUnit> customaryUnits { get; }

        public HashSet<QuantityType> QunatiyTypes { get; }
        public HashSet<SameUnit> SameUnits { get; }
        
       

       
       
       
        

        Dictionary<string,DimensionalClass> dimensionalClasses = new Dictionary<string, DimensionalClass>();

        public XmlHandler()
        {
            unitOfMeasures = new List<UnitOfMeasure>();
            customaryUnits = new List<CustomaryUnit>();
            SameUnits = new HashSet<SameUnit>();
            HashSet<QuantityType> QunatiyTypes = new HashSet<QuantityType>();
            xml=ReadFile();
            CreateUoms();
        }

        public XElement ReadFile()
        {
            var filename = "..\\DatabaseInitializer\\poscUnits22.xml";
            var currentDirectory = Directory.GetCurrentDirectory();
            var filePath = Path.Combine(currentDirectory, filename);
            return XElement.Load(filePath);

        }
        

        public void CreateUoms()
        {
            

            var units = from item in xml.Descendants("UnitOfMeasure") select item;
            Console.WriteLine(units.FirstOrDefault());
            Console.WriteLine(units.Last());
            
            
            foreach (var unit in units)
            {
                //no BaseUnit tag means it is a customary unit
                bool isBaseUnit = unit.Descendants("BaseUnit").Any();
                
                 
                if (!isBaseUnit)
                {
                    CustomaryUnit unitOfMeasure = new CustomaryUnit();
                    CreateUofMeasure(unit,unitOfMeasure);
                    AddCustomaryComponent(unit,unitOfMeasure);
                    customaryUnits.Add(unitOfMeasure);
                }
                else
                {
                    UnitOfMeasure unitOfMeasure = new UnitOfMeasure();
                    CreateUofMeasure(unit,unitOfMeasure);
                    unitOfMeasures.Add(unitOfMeasure);
                }
                

            }

            foreach (var unit in unitOfMeasures)
            {
                    unit.DimensionalClass = dimensionalClasses[unit.DimensionClassId];
            }

            

            //public UnitOfMeasure(string id, string annotation, string name, DimensionalClass dimensionalClass,
            //ICollection<QuantityType> quantityTypes, List<SameUnit> sameUnits)

            //foreach (var v in test) { Console.WriteLine(v); }
        }

        private void AddCustomaryComponent(XElement unit, CustomaryUnit unitOfMeasure)
        {

            string id = (string) unit.Attribute("id");

            var conversion = unit.Descendants("ConversionToBaseUnit").First();

            string baseUnit = (string) conversion.Attribute("baseUnit");
            
            var factor = conversion.Element("Factor");

            if (factor != null)
            {
                unitOfMeasure.ConversionToBaseUnit = new ConversionToBaseUnit(baseUnit, id,
                    0.0,(double) factor, 1.0, 0.0);

                return;
            }

            var fraction = conversion.Element("Fraction");

            if (fraction != null)
            {
                unitOfMeasure.ConversionToBaseUnit = new ConversionToBaseUnit(baseUnit, id,
                    0.0,(double) fraction.Element("Numerator"),(double) fraction.Element("Denominator") , 0.0);
                return;
            }
            
            
            var formula = conversion.Element("Formula");

            if (formula != null)
            {
                unitOfMeasure.ConversionToBaseUnit = new ConversionToBaseUnit(baseUnit, id,
                    (double) formula.Element("A"),
                    (double) formula.Element("B"),
                    (double) formula.Element("C"),
                    (double) formula.Element("D"));
                return;
            }


        }

        private void CreateUofMeasure(XElement unit, UnitOfMeasure unitOfMeasure)
        {
            string id = (string) unit.Attribute("id");
            unitOfMeasure.Id = id;
            unitOfMeasure.Annotation = (string) unit.Attribute("annotation");
            unitOfMeasure.Name = (string) unit.Descendants("Name").First();

            var dim = unit.Descendants("DimensionalClass").FirstOrDefault();
            string dimensionalClassId = dim.Value;

            //AddSameUnits(unit,unitOfMeasure);
           AddQuantityType(unit,unitOfMeasure);
            
            if(!dimensionalClasses.ContainsKey(dimensionalClassId))
            {
                dimensionalClasses.Add(dimensionalClassId,new DimensionalClass(dimensionalClassId) );
            }
                
            dimensionalClasses[dimensionalClassId].Units.Add(unitOfMeasure);

            unitOfMeasure.DimensionClassId = dimensionalClassId;
                
            
        }

        void AddSameUnits(XElement unit, UnitOfMeasure unitOfMeasure)
        {
            var sameUnits = unit.Descendants("SameUnit");
           
            
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
            
        }

        void AddQuantityType(XElement unit, UnitOfMeasure unitOfMeasure)
        {
            
            
            var quantityTypes = unit.Descendants("QuantityType");

            //List<UnitOfMeasureQuantityType>  unitOfMeasureQuantityTypes=  new List<UnitOfMeasureQuantityType>();
            foreach (var qType in quantityTypes)
            {
                if ((string) qType != null)
                {
                    var quantityType = new QuantityType((string) qType);
                    
                    /*
                    var u = new UnitOfMeasureQuantityType
                    {
                        QuantityType = quantityType,
                        UnitOfMeasure = unitOfMeasure,
                        UnitOfMeasureId = unitOfMeasure.Id
                    };
                    u.QuantityType.UnitOfMeasureQuantityTypes = unitOfMeasureQuantityTypes;
                    unitOfMeasureQuantityTypes.Add(u);
                    */
                    QunatiyTypes.Add(quantityType);
                }
                

            }
            

            //unitOfMeasure.UnitOfMeasureQuantityTypes = unitOfMeasureQuantityTypes;
        }



    }
}