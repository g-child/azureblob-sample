using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

namespace KNK.Sample.StorageBlob.Console
{
    class Program
    {
        static async Task Main()
        {
            var storageAccount = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("my-images");

            await container.CreateIfNotExistsAsync();

            var blockBlob = container.GetBlockBlobReference("blobfilename.png");

            using (var stream = File.OpenRead(@"C:\trash\input.png"))
            {
                await blockBlob.UploadFromStreamAsync(stream);
            }

            using (var stream = File.OpenWrite(@"C:\trash\output.png"))
            {
                await blockBlob.DownloadToStreamAsync(stream);
            }
        }
    }
}
