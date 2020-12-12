namespace Data.Models
{
    public class CustomaryUnit : UnitOfMeasure
    {
        public CustomaryUnit()
        { }
        
        
        public ConversionToBaseUnit ConversionToBaseUnit { get; set; }
       // public int ConversionToBaseUnitId { get; set; }
        public string BaseUnitId { get; set; }
    }
}