using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Models
{
    
    public class UnitOfMeasureQuantityType
    {
        public UnitOfMeasureQuantityType() { }

        public UnitOfMeasureQuantityType(QuantityType quantityType,  UnitOfMeasure unitOfMeasure, string unitOfMeasureId)
        {
            QuantityType = quantityType;
            UnitOfMeasure = unitOfMeasure;
            UnitOfMeasureId = unitOfMeasureId;
        }

        public QuantityType QuantityType { get; set; }
        
        public string QuantityTypeId { get; set; }
        
        public UnitOfMeasure UnitOfMeasure { get; set; }
        
        
        public string UnitOfMeasureId { get; set; }
        
        
    }
    
}
