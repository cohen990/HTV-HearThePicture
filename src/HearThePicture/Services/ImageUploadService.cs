using System;
using System.Web;
using HearThePicture.Repositories;

namespace HearThePicture.Services
{
	class ImageUploadService
	{
		private readonly ImageBlobRepository _repo;
		private readonly ImageToSoundConverter _converter;

		public ImageUploadService()
		{
			_converter = new ImageToSoundConverter();
			_repo = new ImageBlobRepository();
		}

		public Uri Upload(HttpPostedFileBase image)
		{
			var imageId = _repo.Store(image.InputStream);

			var uri = _converter.ConvertBlob(imageId, 11025);

			return uri;
		}
	}
}
