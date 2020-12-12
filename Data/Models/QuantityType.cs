using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Data.Models
{
    public class QuantityType
    {
        public QuantityType() { }

        public QuantityType(string notation)
        {
            Notation = notation;
            UnitOfMeasureQuantityTypes = new List<UnitOfMeasureQuantityType>();
        }

        [Key] 
        public string Notation { get; set; }
        public ICollection<UnitOfMeasureQuantityType> UnitOfMeasureQuantityTypes { get; set; }
        
    }
}