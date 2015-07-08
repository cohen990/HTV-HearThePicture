using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace HearThePicture.Repositories
{
	public class ImageBlobRepository
	{
		private CloudBlobContainer Container { get; set; }

		public ImageBlobRepository(bool autoInitialize = true)
		{
			if (autoInitialize)
			{
				Initialize();
			}
		}

		public void Initialize()
		{
			string connectionString = ConfigurationManager.AppSettings["StorageConnectionString"];

			CloudStorageAccount account = CloudStorageAccount.Parse(connectionString);

			CloudBlobClient client = account.CreateCloudBlobClient();

			Container = client.GetContainerReference("images");
			Container.CreateIfNotExists();

			Container.SetPermissions(
				new BlobContainerPermissions
				{
					PublicAccess =
						BlobContainerPublicAccessType.Blob
				});
		}

		public string Store(Stream stream)
		{
			var blobName = Guid.NewGuid().ToString("N");

			CloudBlockBlob blob = Container.GetBlockBlobReference(blobName);

			blob.UploadFromStream(stream);

			return blobName;
		}

		public Stream Get(string blobName)
		{
			var imageBlob = Container.GetBlockBlobReference(blobName);

			Stream result = new MemoryStream();

			imageBlob.DownloadToStream(result);

			return result;
		}
	}
}