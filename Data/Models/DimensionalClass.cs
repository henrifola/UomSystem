using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Data.Models
{
    public class DimensionalClass
    {
        public DimensionalClass() { } //No parameter constructor

        public DimensionalClass(string notation)
        {
            Notation = notation;
            Units = new List<UnitOfMeasure>();
        }
        
        [Key]
        public string Notation { get; set; }
        
        public List<UnitOfMeasure> Units { get; set; }
    }
}