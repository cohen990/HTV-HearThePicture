using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using System.Drawing;
using System.Linq;

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
	internal class GetPixelstests : BitmapLoaderTests
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
	}

	internal class BitmapLoader
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
