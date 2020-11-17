using System.IO;
using System.Xml.Linq;

namespace UomLibrary.XmlHandler
{
    // TODO parsing diagram
    public class XmlHandler : IXmlHandler
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