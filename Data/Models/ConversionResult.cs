using Microsoft.EntityFrameworkCore;

namespace Data.Models
{
    
    public class ConversionResult 
    {
        public double Quantity { get; set; }
        public UnitOfMeasure Unit { get; set; }
    }
}