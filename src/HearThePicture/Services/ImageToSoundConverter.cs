using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HearThePicture.Models;
using HearThePicture.Repositories;

namespace HearThePicture.Services
{
	public class ImageToSoundConverter
	{
		private ImageBlobRepository _repo;

		public ImageToSoundConverter()
		{
			_repo = new ImageBlobRepository();
		}

		public void ConvertLocal(string imageFilePath, int samplesPerPixel = 88200, bool synth = true)
		{
			var loader = new BitmapLoader();
			var file = loader.Load(imageFilePath);
			var bitmap = loader.GetBitmap(file);
			var pixels = loader.GetPixels(bitmap);

			var analyzer = new PixelAnalyzer();

			List<Tone> tones = pixels.Select(analyzer.GetTone).Take(1000).ToList();

			var wavService = new WavService();

			wavService.Play(tones, samplesPerPixel, synth);
		}

		public Uri ConvertBlob(string imageIdentifier, int samplesPerPixel = 88200, bool synth = true)
		{
			var loader = new BitmapLoader();
			Stream image = _repo.Get(imageIdentifier);
			Bitmap bitmap = loader.GetBitmap(image);
			List<Color> pixels = loader.GetPixels(bitmap);

			var analyzer = new PixelAnalyzer();

			List<Tone> tones = pixels.Select(analyzer.GetTone).Take(1000).ToList();

			var wavService = new WavService();

			var uri = wavService.MakeBlob(tones, samplesPerPixel, synth);

			return uri;
		}
	}
}