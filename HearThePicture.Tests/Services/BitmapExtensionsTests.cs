using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using NUnit.Framework;

namespace HearThePicture.Tests.Services
{
	[TestFixture]
	class DownSampleTests
	{
		[Test]
		public void GivenNull_ReturnsNull()
		{
			var bitmap = (Bitmap)null;

			var result = bitmap.DownSample();

			Assert.That(result, Is.Null);
		}

		[Test]
		public void Given1PixelBitmap_Returns1PixelBitmap()
		{
			var bitmap = new Bitmap(1, 1);

			var result = bitmap.DownSample();

			Assert.That(result.Width, Is.EqualTo(1));
			Assert.That(result.Height, Is.EqualTo(1));
		}

		[Test]
		public void Given1PixelBitmap_ReturnsSamePixel()
		{
			var bitmap = new Bitmap(1, 1);

			bitmap.SetPixel(0,0, Color.FromArgb(128, 255, 255, 255));

			var result = bitmap.DownSample();

			Assert.That(result.GetPixel(0, 0).A, Is.EqualTo(bitmap.GetPixel(0, 0).A));
		}

		[Test]
		public void Given2x2PixelBitmap_Returns1x1PixelBitmap()
		{
			var bitmap = new Bitmap(2, 2);

			var result = bitmap.DownSample();

			Assert.That(result.Width, Is.EqualTo(1));
			Assert.That(result.Height, Is.EqualTo(1));
		}

		[Test]
		public void Given2x2PixelBitmap_ReturnsAverage()
		{
			var bitmap = new Bitmap(2, 2);

			bitmap.SetPixel(0, 0, Color.FromArgb(0, 255, 255, 255));
			bitmap.SetPixel(0, 1, Color.FromArgb(63, 255, 255, 255));
			bitmap.SetPixel(1, 0, Color.FromArgb(127, 255, 255, 255));
			bitmap.SetPixel(1, 1, Color.FromArgb(191, 255, 255, 255));

			var result = bitmap.DownSample();

			Assert.That(result.GetPixel(0, 0).A, Is.EqualTo(95));
		}

		[Test]
		public void Given2x1PixelBitmap_Returns1x1PixelBitmap()
		{
			var bitmap = new Bitmap(2, 1);

			var result = bitmap.DownSample();

			Assert.That(result.Width, Is.EqualTo(1));
			Assert.That(result.Height, Is.EqualTo(1));
		}

		[Test]
		public void Given3x3PixelBitmap_Returns1x1PixelBitmap()
		{
			var bitmap = new Bitmap(3, 3);

			var result = bitmap.DownSample();

			Assert.That(result.Width, Is.EqualTo(1));
			Assert.That(result.Height, Is.EqualTo(1));
		}
	}

	public static class BitmapExtensions
	{
		public static Bitmap DownSample(this Bitmap bitmap)
		{
			if (bitmap == null)
				return null;
			if (bitmap.Height == 1 && bitmap.Width == 1)
				return bitmap;

			int newHeight = (int) Math.Floor((double) bitmap.Height/2);
			int newWidth = (int) Math.Floor((double) bitmap.Width/2);

			if (bitmap.Height == 1)
			{
				newHeight = 1;
			}
			if (bitmap.Width == 1)
			{
				newWidth = 1;
			}

			//var pixels = new Color[newWidth][];

			//for (int i = 0; i < newWidth; i++)
			//{
			//	pixels[i] = new Color[newHeight];
			//}

			//for (int x = 0; x < bitmap.Width; x+= 2)
			//{
			//	for (int y = 0; y < bitmap.Height; y+= 2)
			//	{
			//		pixels[x/2][y/2] = new Color();
			//	}
			//}

			var result = new Bitmap(newWidth, newHeight);

			return result;
		}
	}
}
