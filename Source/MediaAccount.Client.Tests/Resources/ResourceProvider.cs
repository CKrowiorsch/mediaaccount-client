using System;
using System.IO;

namespace Krowiorsch.MediaAccount.Resources
{
    public static class ResourceProvider
    {
        public static string ProvideJsonByName(string name)
        {
            var nameWithExtension = name + ".json";

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", nameWithExtension);

            return File.ReadAllText(path);
        }
    }
}