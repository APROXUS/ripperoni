using System;
using System.IO;
using System.Net;
using System.Reflection;

using Downloader;

namespace Ripperoni
{
    public class Global
    {
        public static DownloadConfiguration DownloadOptions = new DownloadConfiguration()
        {
            BufferBlockSize = 10240, // Usually, hosts support max to 8000 bytes, default values is 8000
            ChunkCount = 8, // File parts to download, default value is 1
            MaximumBytesPerSecond = 0, // Download speed limited to 1MB/s, default value is zero or unlimited
            MaxTryAgainOnFailover = int.MaxValue, // The maximum number of times to fail
            OnTheFlyDownload = false, // Caching in-memory or not? default values is true
            ParallelDownload = true, // Download parts of file as parallel or not. Default value is false
            TempDirectory = Path.GetTempPath() + "APROX Ripperoni", // Set the temp path for buffering chunk files, the default path is Path.GetTempPath()
            Timeout = 1000, // Timeout (millisecond) per stream block reader, default values is 1000
            RequestConfiguration = // Configure and customize request headers
            {
                Accept = "*/*",
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                CookieContainer =  new CookieContainer(), // Add your cookies
                Headers = new WebHeaderCollection(), // Add your custom headers
                KeepAlive = false,
                ProtocolVersion = HttpVersion.Version11, // Default value is HTTP 1.1
                UseDefaultCredentials = false,
                UserAgent = $"DownloaderSample/{Assembly.GetExecutingAssembly().GetName().Version.ToString(3)}"
            }
        };

        public static DownloadService Downloader = new DownloadService(DownloadOptions);
    }
}
