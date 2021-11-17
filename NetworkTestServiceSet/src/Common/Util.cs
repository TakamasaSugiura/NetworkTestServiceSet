using System;
using System.IO;
using System.Reflection;

namespace Common
{
    public static class Util
    {
        public static DirectoryInfo ApplicationDir => new FileInfo(Assembly.GetExecutingAssembly().Location).Directory;

        public static string GetFullPathOnApplicationDir(string name)
        {
            return Path.Combine(ApplicationDir.FullName, name);
        }
    }
}
