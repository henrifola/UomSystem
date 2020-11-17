using System.IO;
using System.Xml.Linq;

namespace DatabaseInitializer
{
    // TODO parsing diagram
    public class XmlHandler 
    {
        private XElement data;

        public XmlHandler()
        {
            ReadFile();
        }

        public XElement ReadFile()
        {
            var filename = "poscUnits22.xml";
            var currentDirectory = Directory.GetCurrentDirectory();
            var FilePath = Path.Combine(currentDirectory, filename);
            return XElement.Load(FilePath);

        }
        
        

    }
}