using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileReaderLibrary;
using NUnit.Framework;

namespace FileReaderLibrary_Tests
{
    public class IFileReader_Should
    {
        private const string workingDir = "C:/Users/Gizon/Desktop/config-reader/config-reader/FileReaderLibrary/FilesToRead";
        private const string xmlFileFormat = "*.xml";
        private const string csvFileFormat = "*.csv";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestCsvFileReader_AcceptableFileFormat()
        {
            TestFileReader_AcceptableFileFormat(new CsvFileReader(), csvFileFormat);
        }

        [Test]
        public void TestCsvFileReader_ReadingResults()
        {
            TestFileReader_ReadingResults(new CsvFileReader(),
                new[] {new Configuration {Name = "Конфигурация 2", Description = "Описание Конфигурации 2"}});
        }

        [Test]
        public void TestXmlFileReader_AcceptableFileFormat()
        {
            TestFileReader_AcceptableFileFormat(new XmlFileReader(), xmlFileFormat);
        }

        [Test]
        public void TestXmlFileReader_ReadingResults()
        {
            TestFileReader_ReadingResults(new XmlFileReader(),
                new[] {new Configuration {Name = "Конфигурация 1", Description = "Описание Конфигурации 1"}});
        }

        [Test]
        public void FileReader_ReadingResults_AllReaders()
        {
            var readers = new List<IFileReader> {new CsvFileReader(), new XmlFileReader()};

            var allConfigs = new List<IConfiguration>();
            foreach (var reader in readers)
            {
                allConfigs.AddRange(ReadFileWithFileReader(reader));
            }

            var actualConfigs = new List<IConfiguration>
            {
                new Configuration {Name = "Конфигурация 1", Description = "Описание Конфигурации 1"},
                new Configuration {Name = "Конфигурация 2", Description = "Описание Конфигурации 2"}
            };

            Assert.That(allConfigs.OrderBy(x => x.Name), Is.EqualTo(actualConfigs));
        }

        private static void TestFileReader_AcceptableFileFormat(IFileReader fileReader, string expectedFileFormat)
        {
            var configsByReader = ReadFileWithFileReader(fileReader);

            var configsShould = Directory.EnumerateFiles(workingDir, expectedFileFormat)
                .Select(fileReader.ReadFile).SelectMany(x => x);

            Assert.That(configsShould.Count(), Is.EqualTo(configsByReader.Count()));
        }

        private static void TestFileReader_ReadingResults(IFileReader fileReader, IEnumerable<Configuration> expected)
        {
            var configs = ReadFileWithFileReader(fileReader);

            Assert.That(expected, Is.EqualTo(configs));
        }

        private static IEnumerable<IConfiguration> ReadFileWithFileReader(IFileReader fileReader)
        {
            return Directory.EnumerateFiles(workingDir, fileReader.AcceptableFileFormat)
                .Select(fileReader.ReadFile).SelectMany(x => x);
        }
    }
}