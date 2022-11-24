// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.IO;
using FileReaderLibrary;

var readers = new List<IFileReader> {new CsvFileReader(), new XmlFileReader()};

var commandLineArgs = Environment.GetCommandLineArgs();
string workingDir;
if (commandLineArgs.Length < 2)
{
    workingDir = "FilesToRead";
    Console.WriteLine($"No folder given. Reading from default: {workingDir}");
}
else
{
    workingDir = commandLineArgs[1];
    Console.WriteLine($"Reading from folder: {workingDir}");
}

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