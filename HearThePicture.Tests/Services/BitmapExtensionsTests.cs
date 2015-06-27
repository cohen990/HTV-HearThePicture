using System;
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

			Assert.That(result.Height, Is.EqualTo(1));
			Assert.That(result.Width, Is.EqualTo(1));
		}

		[Test]
		public void Given2x2PixelBitmap_Returns1x1PixelBitmap()
		{
			var bitmap = new Bitmap(2, 2);

			var result = bitmap.DownSample();

			Assert.That(result.Height, Is.EqualTo(1));
			Assert.That(result.Width, Is.EqualTo(1));
		}
	}

	public static class BitmapExtensions
	{
		public static Bitmap DownSample(this Bitmap bitmap)
		{
			return bitmap;
		}
	}
}
