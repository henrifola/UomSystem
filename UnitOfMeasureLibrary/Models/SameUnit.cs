using System.ComponentModel.DataAnnotations;

namespace UomLibrary.Models
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