using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;

namespace ProjetVoltaire
{
    internal class ChromeDriverUpdater
    {
        public static void DownloadLatestChromeDriver()
        {
            string latestVersion = GetLatestChromeDriverVersion();
            if (latestVersion != null)
            {
                
                string downloadUrl = $"https://storage.googleapis.com/chrome-for-testing-public/{latestVersion}/win64/chromedriver-win64.zip";
                string downloadPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "chromedriver-win64.zip");

                using (WebClient client = new())
                {
                    Console.WriteLine($"Downloading Chromedriver version {latestVersion}...");
                    client.DownloadFile(downloadUrl, downloadPath);
                    Console.WriteLine("Chromedriver downloaded successfully.");
                }
                Console.WriteLine("Unzipping Chromedriver...");
                ZipFile.ExtractToDirectory(downloadPath, AppDomain.CurrentDomain.BaseDirectory, true);
                Console.WriteLine("Chromedriver unzipped successfully.");
            }
            else
            {
                Console.WriteLine("Failed to fetch the latest version of Chromedriver.");
            }
        }

        static string GetLatestChromeDriverVersion()
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    string json = client.DownloadString("https://googlechromelabs.github.io/chrome-for-testing/LATEST_RELEASE_STABLE");
                    return json.Trim();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching the latest version of Chromedriver: {ex.Message}");
                return null;
            }
        }
    }
}
