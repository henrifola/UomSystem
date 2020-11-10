using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UomLibrary.Models
{
    public class UnitOfMeasureQuantityType
    {
        public UnitOfMeasureQuantityType() { }
        
        
        [Key]
        [Column(Order=1)]
        public QuantityType QuantityType { get; set; }
        public int QuantityTypeId { get; set; }
        [Key]
        [Column(Order=2)]
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public string UnitOfMeasureId { get; set; }
        
        
    }
    
}