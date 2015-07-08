using System.Configuration;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace HearThePicture.Repositories
{
	public class MusicBlobRepository
	{
		private CloudBlobContainer Container { get; set; }

		public MusicBlobRepository(bool autoInitialize = true)
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

			Container = client.GetContainerReference("musics");
			Container.CreateIfNotExists();

			Container.SetPermissions(
				new BlobContainerPermissions
				{
					PublicAccess =
						BlobContainerPublicAccessType.Blob
				});
		}

		public CloudBlobStream GetStream(string fileId, out string blobReference)
		{
			blobReference = fileId + ".wav";
			var blob = Container.GetBlockBlobReference(blobReference);

			CloudBlobStream stream = blob.OpenWrite();

			return stream;
		}

		public Stream Get(string blobReference)
		{
			var blob = Container.GetBlockBlobReference(blobReference);

			blob.Properties.ContentType = "audio/wav";

			blob.SetProperties();

			Stream result = new MemoryStream();

			blob.DownloadToStream(result);

			return result;
		}
	}
}