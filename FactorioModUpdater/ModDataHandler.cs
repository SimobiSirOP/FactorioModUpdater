using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using FactorioModUpdater.Files;
using FactorioModUpdater.Helpers;

namespace FactorioModUpdater;

public static class ModDataHandler
{
    public static void ReadAllMods()
    {
        string[] modFiles = Directory.GetFiles(Master.ModFolderPath);

        foreach (string modFile in modFiles)
        {
            ModInfo? modInfo;
            
            if (Path.GetExtension(modFile) != ".zip") continue;
            using (ZipArchive archive = ZipFile.OpenRead(modFile))
            {
                ZipArchiveEntry? modEntry = archive.Entries.FirstOrDefault(e => e.Name == "info.json");
                if (modEntry == null)
                {
                    Console.WriteLine($"Info of mod {modFile} not found.");
                    continue;
                }
                using (StreamReader fileStream = new StreamReader(modEntry.Open()))
                {
                    modInfo = Serializer.SerializeFromString<ModInfo>(fileStream.ReadToEnd());
                }
            }
            
            if (modInfo == null) continue;
            HandleMod(modFile, modInfo).GetAwaiter().GetResult(); 
        }
    }

    public static void InitiateModData()
    {
        ReadAllMods();
    }

    public static async Task HandleMod(string pathToMod, ModInfo modInfo)
    {
        Console.WriteLine(modInfo.Name + " - " + modInfo.Version + "\n");
        ModApiInfo modApiInfo =  await GetLatestModInfo(modInfo);

        ReleaseInfo[] releases = modApiInfo.Releases.Where(r => r.InfoJson.FactorioVersion == modInfo.FactorioVersion).ToArray();
        ReleaseInfo downloadedRelease = releases.First(r => r.Version == modInfo.Version);

        ReleaseInfo latestRelease = downloadedRelease;
        foreach (var release in releases)
        {
            if (release.ReleaseDate >= downloadedRelease.ReleaseDate)
                latestRelease = release;
        }

        if (latestRelease == downloadedRelease)
        {Console.WriteLine("Mod is up to date.");
            return;
        }
        Console.WriteLine("Found latest release: " + latestRelease.Version);
        
        await DownloadMod(pathToMod, latestRelease);

    }

    private static readonly string modRequestDataUrl = "";
    private static async Task<ModApiInfo> GetLatestModInfo(ModInfo modInfo)
    {
        HttpResponseMessage httpResponse = await Master.ModHttpClient.GetAsync("api/mods/" + modInfo.Name);
        string httpResponseString = await httpResponse.Content.ReadAsStringAsync();
        return Serializer.SerializeFromString<ModApiInfo>(httpResponseString);
    }


    private static async Task DownloadMod(string oldPath, ReleaseInfo release)
    {
        Console.WriteLine($"Downloading {release.Version}");
        string requestUri =
            $"{release.DownloadUrl}?username={Master.FactorioUsername}&token={Master.FactorioAuthToken}";
        HttpResponseMessage responseMessage = await Master.ModHttpClient.GetAsync(requestUri);
        responseMessage.EnsureSuccessStatusCode();

        using (Stream stream = await responseMessage.Content.ReadAsStreamAsync())
        {
            FileStream fileStream = new FileStream(Path.Combine(Master.ModFolderPath, release.FileName), FileMode.Create);
            stream.CopyTo(fileStream);
            fileStream.Close();
        }
        
        Console.WriteLine($"\nDownloaded {release.Version}");
        File.Delete(oldPath);
    }
}