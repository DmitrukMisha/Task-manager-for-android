using System;
using System.Runtime.CompilerServices;
using Android.OS;
using Task_Manager;
using System.IO;

namespace Task_Manager
{
    public class AndroidDbPath : IPath
    {
        public string GetDatabasePath(string filename)
        {
            return Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), filename);
        }
    }
}