using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace BlobQuickstartV12
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Azure Blob Storage v12 - .NET quickstart sample\n");
            string connectionString =
                "DefaultEndpointsProtocol=https;AccountName=scottstoragesamples;AccountKey=KEnMQgDAP0o8ISRnt97+nW7XfJQcZOc8lX50C/SwtuDERtto/+I8r4ynYNKbvuBzg9hO3yWD1gfKEQaUiaTbkA==;EndpointSuffix=core.windows.net";
            
            // Create a BlobServiceClient object which will be used to create a container client
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            //Create a unique name for the container
            string containerName = "quickstartblobs" + Guid.NewGuid().ToString();

            // Create the container and return a container client object
            BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);
            
            // Create a local file in the ./data/ directory for uploading and downloading
            string localPath = "./data/";
            string fileName = "PXL_20220402_020318955.jpg";
            string localFilePath = Path.Combine(localPath, fileName);
            
            // Get a reference to a blob
            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            
            Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);
            
            // Upload data from the local file
            await blobClient.UploadAsync(localFilePath, true);
            
            Console.WriteLine("Listing blobs...");

            // List all blobs in the container
            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
            {
                Console.WriteLine("\t" + blobItem.Name);
            }
        }
    }
}