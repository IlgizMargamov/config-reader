using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace FileReaderLibrary
{
    public class XmlFileReader : IFileReader
    {
        public string AcceptableFileFormat { get; }

        public XmlFileReader()
        {
            AcceptableFileFormat = "*.xml";
        }
        
        public IEnumerable<IConfiguration> ReadFile(string path)
        {
            var document = new XmlDocument();
            try
            {
                document.Load(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Enumerable.Empty<IConfiguration>();
            }

            var root = document.DocumentElement;
            var config = new Configuration();
            if (root == null) return Enumerable.Empty<IConfiguration>();//throw new ArgumentException($"File at {path} is not in xml format");
            foreach (XmlNode element in root.ChildNodes)
            {
                switch (element.Name)
                {
                    case "name":
                        config.Name = element.InnerText;
                        break;
                    case "description":
                        config.Description = element.InnerText;
                        break;
                }

            }

            return config.Name.Length > 0 && config.Description.Length > 0
                ? new[] {config}
                : Enumerable.Empty<IConfiguration>();
        }
    }
}