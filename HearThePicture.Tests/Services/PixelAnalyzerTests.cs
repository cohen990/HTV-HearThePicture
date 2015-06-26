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
		//[Test]
		//public void GivenColorWith0000_ReturnsHueOf0()
		//{
		//	var result = Analyzer.GetFrequency(Color.FromArgb(0, 0, 0, 0));

		//	Assert.That(result, Is.EqualTo(0));
		//}

		//[Test]
		//public void GivenRed_ReturnsHueOf0()
		//{
		//	var result = Analyzer.GetFrequency(Color.FromArgb(255, 255, 0, 0));

		//	Assert.That(result, Is.EqualTo(0));
		//}

		//[Test]
		//public void GivenGreen_ReturnsHueOf120()
		//{
		//	var result = Analyzer.GetFrequency(Color.FromArgb(255, 0, 255, 0));

		//	Assert.That(result, Is.EqualTo(120));
		//}
	}

	[TestFixture]
	class ConvertToFrequencyTests : PixelAnalyzerTests
	{
		[Test]
		public void Given0_Returns80()
		{
			var result = Analyzer.ConvertToFrequency(0);

			Assert.That(result, Is.EqualTo(80));
		}

		[Test]
		public void Given360_Returns1200()
		{
			var result = Analyzer.ConvertToFrequency(360);

			Assert.That(result, Is.EqualTo(1200));
		}

		[Test]
		public void Given180_Returns309()
		{
			var result = Analyzer.ConvertToFrequency(180);

			Assert.That(result, Is.EqualTo(309.83866769659318d));
		}

		[Test]
		public void Given270_Returnsabout500()
		{
			var result = Analyzer.ConvertToFrequency(270);

			Assert.That(result, Is.EqualTo(609.75929778553746d));
		}
	}

	internal class PixelAnalyzer
	{
		private const float MinFrequency = 80;
		private const float MaxFrequency = 1200;

		public float GetFrequency(Color pixel)
		{
			float hue = pixel.GetHue();

			return hue;
		}

		public double ConvertToFrequency(int hue)
		{
			if(hue == 0)
				return MinFrequency;
			if(hue == 360)
				return MaxFrequency;

			var min = Math.Log10(MinFrequency);
			var max = Math.Log10(MaxFrequency);

			var temp = (hue/360.0)*(max - min) + min;

			double frequency = Math.Pow(10, temp);

			return frequency;
		}
	}
}
