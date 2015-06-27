using System.Linq;

namespace HearThePicture.Services
{
	public class ImageToSoundConverter
	{
		public void Convert(string imageFilePath)
		{
			var loader = new BitmapLoader();
			var file = loader.Load(imageFilePath);
			var bitmap = loader.GetBitmap(file);
			var pixels = loader.GetPixels(bitmap);

			var analyzer = new PixelAnalyzer();

			double frequency = analyzer.GetFrequency(pixels.First());

			var wavService = new WavService();

			string fileName = "red.wav";
			wavService.Play(frequency);
		}
	}
}