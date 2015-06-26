using System.Drawing;
using HearThePicture.Services;
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
	class GetFrequencyTests : PixelAnalyzerTests
	{
		[Test]
		public void GivenColorWith0000_ReturnsFrequencyAround80()
		{
			var result = Analyzer.GetFrequency(Color.FromArgb(0, 0, 0, 0));

			Assert.That(result, Is.InRange(79, 81));
		}

		[Test]
		public void GivenRed_ReturnsFrequencyAround80()
		{
			var result = Analyzer.GetFrequency(Color.Red);

			Assert.That(result, Is.InRange(79, 81));
		}

		[Test]
		public void GivenGreen_ReturnsFrequencyAround197()
		{
			var result = Analyzer.GetFrequency(Color.FromArgb(255, 0, 255, 0));

			Assert.That(result, Is.InRange(197, 198));
		}

		[Test]
		public void GivenTeal_ReturnsFrequencyAround309()
		{
			var result = Analyzer.GetFrequency(Color.FromArgb(255, 0, 250, 250));

			Assert.That(result, Is.InRange(308, 310));
		}
	}

	[TestFixture]
	class ConvertToFrequencyTests : PixelAnalyzerTests
	{
		[Test]
		public void Given0_ReturnsAbout80()
		{
			var result = Analyzer.ConvertToFrequency(0);

			Assert.That(result, Is.InRange(79, 81));
		}

		[Test]
		public void Given360_ReturnsAbout1200()
		{
			var result = Analyzer.ConvertToFrequency(360);
			
			Assert.That(result, Is.InRange(1199, 1201));
		}

		[Test]
		public void Given180_ReturnsAbout309()
		{
			var result = Analyzer.ConvertToFrequency(180);
			
			Assert.That(result, Is.InRange(308, 310));
		}

		[Test]
		public void Given270_Returnsabout500()
		{
			var result = Analyzer.ConvertToFrequency(270);

			Assert.That(result, Is.InRange(608, 610));
		}
	}
}
