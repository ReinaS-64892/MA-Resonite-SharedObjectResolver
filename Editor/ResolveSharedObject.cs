using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace net.rs64.MAResonite.SharedObjectResolver
{
    internal static class ResolveSharedObject
    {
        [InitializeOnLoadMethod]
        internal static void Run() { _ = Task.Run(DoResolve); }
        internal const string MA_RESOPUPPET_DIR = "Packages/nadena.dev.modular-avatar.resonite/ResoPuppet~";
        internal static async Task DoResolve()
        {
            var outDir = Path.GetFullPath(MA_RESOPUPPET_DIR);
            var resolvedMarker = Path.Combine(outDir, "SharedObjectResolvedMarker.txt");
            if (File.Exists(resolvedMarker))
            {
                using var reader = File.OpenText(resolvedMarker);
                var beforeResolveBumpVersionString = await reader.ReadLineAsync();
                if (int.TryParse(beforeResolveBumpVersionString, out var result))
                    if (result == ResolveBumpVersion)
                        return;
            }
            await File.WriteAllTextAsync(resolvedMarker, ResolveBumpVersion + "\nSharedObjectResolved! generate this text from MA-Resonite-SharedObjectResolver.");

            using var httpClient = new HttpClient();
            async Task GetLibFromZip(string url, IEnumerable<string> libEntry)
            {

                var nugetPackage = await httpClient.GetAsync(url);
                using var unGetZip = new ZipArchive(await nugetPackage.Content.ReadAsStreamAsync(), ZipArchiveMode.Read);

                foreach (var entryPath in libEntry)
                {
                    var entry = unGetZip.GetEntry(entryPath);
                    if (entry is null)
                    {
                        Debug.Log("lib entry not found: " + entryPath);
                        continue;
                    }

                    entry.ExtractToFile(Path.Combine(outDir, Path.GetFileName(entryPath)), true);
                }
            }

            Task.WaitAll(NeedSharedObjects().Select(a => GetLibFromZip(a.url, a.libEntry)).ToArray());
        }

        internal const int ResolveBumpVersion = 1;
        static IEnumerable<(string url, IEnumerable<string> libEntry)> NeedSharedObjects()
        {
            yield return (
                "https://www.nuget.org/api/v2/package/Ultz.Native.Assimp/5.4.1",
                new[] { "runtimes/linux-x64/native/libassimp.so.5" }
                );

            yield return (
                "https://www.nuget.org/api/v2/package/FreeImage-dotnet-core/4.3.6",
                new[] { "runtimes/ubuntu.16.04-x64/native/FreeImage.so" }
                );

            yield return (
                "https://www.nuget.org/api/v2/package/OpenRA-Freetype6/1.0.11",
                new[] { "native/linux-x64/freetype6.so" }
                );

            yield return (
                "https://www.nuget.org/api/v2/package/SteamAudio.NET.Natives/4.5.3",
                new[] { "runtimes/linux-x64/native/libphonon.so" }
                );
            yield return (
                "https://www.nuget.org/api/v2/package/Brotli.NET/2.1.1",
                new[] { "runtimes/linux-x64/native/brolib_x64.so" }
                );
        }
    }
}
