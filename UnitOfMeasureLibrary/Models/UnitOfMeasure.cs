using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UomLibrary.Models
{
    public class UnitOfMeasure
    {
        public UnitOfMeasure() { }

        public UnitOfMeasure(string id, string annotation, string name, DimensionalClass dimensionalClass,
            ICollection<QuantityType> quantityTypes, List<SameUnit> sameUnits)
        {
            Id = id;
            Annotation = annotation;
            Name = name;

            try
            {
                dimensionalClass.Units.Add(this);
                DimensionClassId = dimensionalClass.Notation;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            
            DimensionalClass = dimensionalClass;
            
            UnitOfMeasureQuantityTypes = new List<UnitOfMeasureQuantityType>();

            SameUnits = sameUnits;

        }
        
        
        public string Annotation { get; set; }
        public DimensionalClass DimensionalClass { get; set; }
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        
        public string DimensionClassId { get; set; }
        public ICollection<UnitOfMeasureQuantityType> UnitOfMeasureQuantityTypes { get; set; }
        public List<SameUnit> SameUnits { get; set; }
    }
}