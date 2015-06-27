using System;
using System.IO;
using NUnit.Framework;
using System.Drawing;
using System.Linq;
using HearThePicture.Services;

namespace HearThePicture.Tests.Services
{
	internal class BitmapLoaderTests
	{
		internal BitmapLoader Loader { get; set; }

		[SetUp]
		public void SetUp()
		{
			Loader = new BitmapLoader();
		}
	}

	[TestFixture]
	internal class LoadTests : BitmapLoaderTests
	{
		[Test]
		public void GivenNoFilePath_ThrowsArgumentException()
		{
			var filePath = string.Empty;

			Assert.Throws<ArgumentException>(() => Loader.Load(filePath));
		}

		[Test]
		public void FindsNoFile_ThrowsFileNotFoundException()
		{
			var filePath = "poo.bmp";

			Assert.Throws<FileNotFoundException>(() => Loader.Load(filePath));
		}

		[Test]
		public void GivenExistingFile_FindsAndReturnsAFile()
		{
			var filePath = "Assets/SinglePixel.bmp";

			var file = Loader.Load(filePath);

			Assert.That(file, Is.Not.Null);
		}
	}

	[TestFixture]
	internal class GetBitmapTests : BitmapLoaderTests
	{
		[Test]
		public void GivenNull_ReturnsNull()
		{
			var result = Loader.GetBitmap(null);
			Assert.That(result, Is.Null);
		}

		[Test]
		public void GivenBMPFileStream_ReturnsBitmap()
		{
			var filePath = "Assets/SinglePixel.bmp";

			var file = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

			var result = Loader.GetBitmap(file);

			Assert.That(result, Is.Not.Null);
		}
	}

	[TestFixture]
	internal class GetPixelsTests : BitmapLoaderTests
	{
		[Test]
		public void GivenNull_ReturnsEmptyList()
		{
			var result = Loader.GetPixels(null);

			Assert.That(result, Is.Empty);
		}

		[Test]
		public void GivenBitmapOfSinglePixel_ReturnsSinglePixel()
		{
			var image = new Bitmap(Image.FromFile("Assets/SinglePixel.bmp"));

			var result = Loader.GetPixels(image);

			Assert.That(result.Count, Is.EqualTo(1));
		}

		[Test]
		public void GivenBitmapOfSingleRedPixel_GetsR255()
		{
			var image = new Bitmap(Image.FromFile("Assets/SinglePixel.bmp"));

			var result = Loader.GetPixels(image);

			Assert.That(result.First().R, Is.EqualTo(255));
		}

		[Test]
		public void GivenBitmapOfTwoGreenPixels_Gets2Pixels()
		{
			var image = new Bitmap(Image.FromFile("Assets/2x1Green.bmp"));

			var result = Loader.GetPixels(image);

			Assert.That(result.Count, Is.EqualTo(2));
		}

		[Test]
		public void GivenBitmapOf4PurplePixels_Gets4Pixels()
		{
			var image = new Bitmap(Image.FromFile("Assets/2x2Purple.bmp"));

			var result = Loader.GetPixels(image);

			Assert.That(result.Count, Is.EqualTo(4));
		}
	}
}
