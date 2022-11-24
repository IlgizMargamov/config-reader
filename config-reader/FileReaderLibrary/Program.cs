// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.IO;
using FileReaderLibrary;

var readers = new List<IFileReader> {new CsvFileReader(), new XmlFileReader()};

const string workingDir = "C:/Users/Gizon/Desktop/config-reader/config-reader/FileReaderLibrary/FilesToRead";

var configs = new List<IConfiguration>();
foreach (var reader in readers)
{
    foreach (var filePath in Directory.EnumerateFiles(workingDir, reader.AcceptableFileFormat))
    {
        configs.AddRange(reader.ReadFile(filePath));
    }
}

foreach (var config in configs)
{
    Console.WriteLine(config.ToString());
}