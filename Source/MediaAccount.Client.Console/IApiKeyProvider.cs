using System;
using System.IO;

namespace Krowiorsch.MediaAccount;

interface IApiKeyProvider
{
    string Provide();
}

class ManualKeyProvider : IApiKeyProvider
{
    string _key;

    public string Provide()
    {
        if (!string.IsNullOrEmpty(_key))
            return _key;

        Console.WriteLine("Please provide Api Key:");
        return _key = Console.ReadLine();
    }
}

class FileApiKeyProvider : IApiKeyProvider
{
    readonly FileInfo _fileInfo;

    public FileApiKeyProvider(FileInfo apiFile) => _fileInfo = apiFile;

    public string Provide() => File.ReadAllText(_fileInfo.FullName);
}