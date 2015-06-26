using System;
using System.Drawing;
using NUnit.Framework;

namespace HearThePicture.Tests.Services
{
	class PixelAnalyzerTests
	{
		public PixelAnalyzer Analyzer { get; set; }

		[SetUp]
		public void SetUp()
		{
			Analyzer = new PixelAnalyzer();
		}
	}

	[TestFixture]
	class GetHueTests : PixelAnalyzerTests
	{
		[Test]
		public void GivenColorWith0000_ReturnsHueOf0()
		{
			var result = Analyzer.GetHue(Color.FromArgb(0, 0, 0, 0));

			Assert.That(result, Is.EqualTo(0));
		}

		[Test]
		public void GivenRed_ReturnsHueOf360()
		{
			var result = Analyzer.GetHue(Color.FromArgb(255,255,0,0));

			Assert.That(result, Is.EqualTo(360));
		}
	}

	internal class PixelAnalyzer
	{
		public float GetHue(Color pixel)
		{
			float hue = pixel.GetHue();
			float sat = pixel.GetSaturation();
			float light = pixel.GetBrightness();

			return hue;
		}
	}
}
