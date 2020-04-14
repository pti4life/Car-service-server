using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Car_service.Modells;

namespace Car_service.Repositories
{
    public static class WorkRepository
    {
        public static IList<Work> GetWorks() 
        {
            var appDataPath = GetAppDataPath();

            if (File.Exists(appDataPath))
            {
                var rawContent = File.ReadAllText(appDataPath);
                var works = JsonSerializer.Deserialize<List<Work>>(rawContent);
                return works;
            }
            else
            {
                return new List<Work>();
            }
        }

        public static void StoreWorks(IList<Work> works)
        {
            var appDataPath = GetAppDataPath();
            var rowContent = JsonSerializer.Serialize(works);
            File.WriteAllText(appDataPath, rowContent);
        }

        public static string GetAppDataPath()
        {
            var localAppFolder = GetLocalFolder();
            var appDataPath = Path.Combine(localAppFolder, "Works.json");
            return appDataPath;
        }

        public static string GetLocalFolder()
        {
            var localAppDataFolder =  Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var localAppFolder = Path.Combine(localAppDataFolder, "Car-service");

            if(!Directory.Exists(localAppFolder))
            {
                Directory.CreateDirectory(localAppFolder);
            }

            return localAppFolder;
        }
    }
}
