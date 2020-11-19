using System.Collections.Generic;
using System.Xml.Linq;
using Data.Models;

namespace DatabaseInitializer
{
    public interface IXmlHandler
    {
        public List<UnitOfMeasure> CreateUoms();
        
    }
}