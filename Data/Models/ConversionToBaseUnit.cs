using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class ConversionToBaseUnit
    {
        public ConversionToBaseUnit() { }

        public ConversionToBaseUnit(string baseUnit, double a, double b, double c, double d)
        {
            A = a;
            B = b;
            C = c;
            D = d;

            BaseUnit = baseUnit;

        }

        
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }
        public double D { get; set; }
        
        [Key]
        public string BaseUnit { get; set; }
        
        
    }
}