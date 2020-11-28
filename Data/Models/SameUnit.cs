using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Data.Models
{
    
    public class SameUnit
    {
        public SameUnit() {}

        public SameUnit(string unitId)
        {
            UnitId = unitId;
        }
        
        
        [Key]
        public string UnitId { get; set; }
    }
}