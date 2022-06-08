using System.IO;
using System.Linq;
using Microsoft.Win32;

namespace MyWPFApp.Model
{
    public class Settings
    {
        public string FilenameSave { get; set; }
        public string? FolderPath { get; set; }
        public bool IsSaveAvb { get; set; }
        public Settings()
        {
            FilenameSave = "\\WalletSave.xml";
            IsSaveAvb = false;
            LoadFile();
        }
        public DirectoryInfo TryGetSolutionDirectoryInfo(string? currentPath = null)
        {
            var directory = new DirectoryInfo(
                currentPath ?? Directory.GetCurrentDirectory());
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }
           return directory;
        }

        public void LoadFile()
        {
            string? test = null;
            RegistryKey? import = Registry.CurrentUser.OpenSubKey(@"Software\FakeTrading",true);
            if (import != null)
            {
                test = import.GetValue("Path")?.ToString();
            }

            if (Directory.Exists(test) && File.Exists(test + FilenameSave))
            {
                FolderPath = test;
                FilenameSave = test + FilenameSave;
                IsSaveAvb = true;
            }
            else
            {
                string FullPath;
                RegistryKey openKey = Registry.CurrentUser.CreateSubKey("Software");
                RegistryKey updKey = openKey.CreateSubKey("FakeTrading");
                FullPath = TryGetSolutionDirectoryInfo().FullName;
                FullPath += "\\MyWPFApp\\Data";
                FilenameSave = FullPath + "\\WalletSave.xml";
                FolderPath = FullPath;
                updKey.SetValue("Path", FolderPath);  // save file on regedit
                updKey.Close();
                openKey.Close();
                IsSaveAvb = false;
            }
        }
        public void ChangeFilename(string filename)
        {
            if (!Directory.Exists(filename)) return;
            string destFile = System.IO.Path.Combine(filename, "WalletSave.xml");
            File.Copy(FilenameSave, destFile, true);//copy file
            File.Delete(FilenameSave);//delete old file
            RegistryKey? openKey = Registry.CurrentUser.OpenSubKey("Software");
            RegistryKey? updKey = openKey?.OpenSubKey("FakeTrading",true);
            FilenameSave = System.IO.Path.Combine(filename, "WalletSave.xml");
            FolderPath = filename;
            updKey?.SetValue("Path", filename);
            updKey?.Close();
            openKey?.Close();
            
        }

        public void DeleteSave()
        {
            File.Delete(FilenameSave);
            RegistryKey? import = Registry.CurrentUser.OpenSubKey(@"Software", true);
            import?.DeleteSubKey("FakeTrading");
            FilenameSave = "\\WalletSave.xml";
            LoadFile();
        }
    }
}
