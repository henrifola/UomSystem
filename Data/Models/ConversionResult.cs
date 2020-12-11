using Microsoft.EntityFrameworkCore;

namespace Data.Models
{
    //ikke db felt
    
    public class ConversionResult 
    {
        public ConversionResult(double quantity, UnitOfMeasure unit)
        {
            Quantity = quantity;
            Unit = unit;
        }

        public double Quantity { get; set; }
        public UnitOfMeasure Unit { get; set; }
    }
}