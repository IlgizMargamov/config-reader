using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace FileReaderLibrary
{
    public class CsvFileReader : IFileReader
    {
        public string AcceptableFileFormat { get; }

        private readonly CsvConfiguration csvConfig;

        public CsvFileReader()
        {
            AcceptableFileFormat = "*.csv";
            
            csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                Delimiter = ";",
            };
   
        }
        
        public IEnumerable<IConfiguration> ReadFile(string path)
        {
            try
            {
                using var streamReader = File.OpenText(path);
                using var csvReader = new CsvReader(streamReader, csvConfig);

                return csvReader.GetRecords<Configuration>().ToList(); // Without .ToList()
                                                                       // csvReader doesn't enumerate values
                                                                       // and flushes(?)
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Enumerable.Empty<IConfiguration>();
            }
        }
    }
}