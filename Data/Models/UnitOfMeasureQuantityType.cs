using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Models
{
    
    public class UnitOfMeasureQuantityType
    {
        public UnitOfMeasureQuantityType() { }
        
        
        
        public QuantityType QuantityType { get; set; }
        [Key,Column(Order=0)]
        public int QuantityTypeId { get; set; }
        
        public UnitOfMeasure UnitOfMeasure { get; set; }
        
        //[Key,Column(Order = 1)]
        public string UnitOfMeasureId { get; set; }
        
        
    }
    
}