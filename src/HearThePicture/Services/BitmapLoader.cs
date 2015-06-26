using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace HearThePicture.Services
{
	public class BitmapLoader
	{
		public FileStream Load(string filePath)
		{
			if (string.IsNullOrWhiteSpace(filePath))
			{
				throw new ArgumentException("You must provide a filePath", "filePath");
			}

			FileStream file = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

			return file;
		}

		public Bitmap GetBitmap(FileStream stream)
		{
			if (stream == null)
				return null;

			Image image = Image.FromStream(stream);

			Bitmap bitmap = new Bitmap(image);

			return bitmap;
		}

		public List<Color> GetPixels(Bitmap image)
		{
			if(image == null)
				return new List<Color>();

			var pixel = image.GetPixel(0, 0);

			return new List<Color> {pixel};
		}
	}
}