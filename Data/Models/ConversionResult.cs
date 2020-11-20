using Microsoft.EntityFrameworkCore;

namespace Data.Models
{
    //ikke db felt
    
    public class ConversionResult 
    {
        public double Quantity { get; set; }
        public UnitOfMeasure Unit { get; set; }
    }
}