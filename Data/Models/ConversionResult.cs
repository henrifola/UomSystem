
namespace Data.Models
{
    public class ConversionResult 
    {
        public ConversionResult(double quantity, string unitId, string annotation)
        {
            Quantity = quantity;
            UnitId = unitId;
            Annotation = annotation;
        }
        public double Quantity { get; set; }
        public string UnitId { get; set; }
        
        public string Annotation { get; set;}
    }
}