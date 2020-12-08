using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Data.Models
{
    public class ConversionToBaseUnit
    {
        public ConversionToBaseUnit()
        {
            
        }

        public ConversionToBaseUnit(string baseUnit,string originUnit, double a, double b, double c, double d)
        {
            A = a;
            B = b;
            C = c;
            D = d;
            
            OriginUnit = originUnit;
            BaseUnit = baseUnit;

        }

        public Double Convert(Double quantity)
        {
            return (A + B * quantity) / (C + D * quantity);
        }

        
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }
        public double D { get; set; }
        
        
        public string BaseUnit { get; set; }
        [Key]
        public string OriginUnit { get; set; }
        
        
    }
}