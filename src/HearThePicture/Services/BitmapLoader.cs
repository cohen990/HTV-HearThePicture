using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web;

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

		public FileStream GetStream(HttpPostedFileBase file)
		{
			if(file == null)
				throw new ArgumentNullException("file");

			var stream = file.InputStream as FileStream;

			return stream;
		}

		public Bitmap GetBitmap(Stream stream)
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

			var pixels = new List<Color>();

			for (int x = 0; x < image.Width; x++)
			{
				for (int y = 0; y < image.Height; y++)
				{
					pixels.Add(image.GetPixel(x, y));
				}
			}

			return pixels;
		}
	}
}