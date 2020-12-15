using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Data.Models;

namespace DatabaseInitializer
{
    public class XmlHandler 
    {
        private readonly XElement xml;
        public List<UnitOfMeasure> UnitOfMeasures  {get;}
        public List<CustomaryUnit> CustomaryUnits { get; }

        public Dictionary<String,QuantityType> QuantityTypesDict { get; }
        public HashSet<SameUnit> SameUnits { get; }

        Dictionary<string,DimensionalClass> dimensionalClasses = new Dictionary<string, DimensionalClass>();

        public XmlHandler()
        {
            
            UnitOfMeasures = new List<UnitOfMeasure>();
            CustomaryUnits = new List<CustomaryUnit>();
            SameUnits = new HashSet<SameUnit>();
            QuantityTypesDict = new Dictionary<String,QuantityType>();
            xml=ReadFile();
            CreateUoms();
        }

        public XElement ReadFile()
        {
            var filename = "DatabaseInitializer/poscUnits22.xml";
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
                    unitOfMeasure.BaseUnitId=(string) unit.Attribute("id");
                    CreateUofMeasure(unit,unitOfMeasure);
                    AddCustomaryComponent(unit,unitOfMeasure);
                    CustomaryUnits.Add(unitOfMeasure);
                }
                else
                {
                    UnitOfMeasure unitOfMeasure = new UnitOfMeasure();
                    CreateUofMeasure(unit,unitOfMeasure);
                    UnitOfMeasures.Add(unitOfMeasure);
                }
                

            }

            foreach (var unit in UnitOfMeasures)
            {
                    unit.DimensionalClass = dimensionalClasses[unit.DimensionClassId];
            }
            
        }

        private void AddCustomaryComponent(XElement unit, CustomaryUnit unitOfMeasure)
        {

            string id = (string) unit.Attribute("id");

            var conversion = unit.Descendants("ConversionToBaseUnit").First();

            string baseUnit = (string) conversion.Attribute("baseUnit");
            unitOfMeasure.BaseUnitId = baseUnit;
            
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
                string q = (string) qType;
                if (q == null) continue;
                
                
                QuantityType quantityType;
                if (QuantityTypesDict.ContainsKey(q))
                {
                    quantityType = QuantityTypesDict[q];
                }
                else
                {
                    quantityType = new QuantityType((string) qType);
                    QuantityTypesDict[q] = quantityType;
                }
                    
                    
                var u = new UnitOfMeasureQuantityType
                {
                    QuantityType = quantityType,
                    UnitOfMeasure = unitOfMeasure,
                    UnitOfMeasureId = unitOfMeasure.Id,
                    QuantityTypeId = q
                };
                //u.QuantityType.UnitOfMeasureQuantityTypes.Add(u);
                    
                unitOfMeasure.UnitOfMeasureQuantityTypes.Add(u);
                    
                //QunatiyTypes.Add(quantityType);
                
            }

        }



    }
}