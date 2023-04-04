using System;
using System.IO;

namespace Krowiorsch.MediaAccount;

interface IApiKeyProvider
{
    string Provide();
}

class ManualKeyProvider : IApiKeyProvider
{
    public string Provide()
    {
        Console.WriteLine("Please provide Api Key:");
        return Console.ReadLine();
    }
}

class FileApiKeyProvider : IApiKeyProvider
{
    readonly FileInfo _fileInfo;

    public FileApiKeyProvider(FileInfo apiFile) => _fileInfo = apiFile;

    public string Provide() => File.ReadAllText(_fileInfo.FullName);
}