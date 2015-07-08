using System.Web;
using HearThePicture.Repositories;

namespace HearThePicture.Services
{
	class ImageUploadService
	{
		private readonly ImageBlobRepository _repo;
		private ImageToSoundConverter _converter;

		public ImageUploadService()
		{
			_converter = new ImageToSoundConverter();
			_repo = new ImageBlobRepository();
		}

		public void Upload(HttpPostedFileBase image)
		{
			var imageId = _repo.Store(image.InputStream);

			_converter.ConvertBlob(imageId, 11025);
		}
	}
}
