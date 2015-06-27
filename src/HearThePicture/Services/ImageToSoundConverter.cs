using System.Linq;

namespace HearThePicture.Services
{
	public class ImageToSoundConverter
	{
		public void Convert(string imageFilePath, int samplesPerPixel = 88200)
		{
			var loader = new BitmapLoader();
			var file = loader.Load(imageFilePath);
			var bitmap = loader.GetBitmap(file);
			var pixels = loader.GetPixels(bitmap);

			var analyzer = new PixelAnalyzer();

			var frequencies = pixels.Select(analyzer.GetFrequency).Take(1000).ToList();

			var wavService = new WavService();

			wavService.Play(frequencies, samplesPerPixel);
		}
	}
}