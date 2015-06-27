using System.Collections.Generic;
using System.Linq;
using HearThePicture.Models;

namespace HearThePicture.Services
{
	public class ImageToSoundConverter
	{
		public void Convert(string imageFilePath, int samplesPerPixel = 88200, bool synth = true)
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
	}
}