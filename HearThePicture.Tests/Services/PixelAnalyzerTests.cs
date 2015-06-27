using System.Drawing;
using HearThePicture.Models;
using HearThePicture.Services;
using NUnit.Framework;

namespace HearThePicture.Tests.Services
{
	internal class PixelAnalyzerTests
	{
		public PixelAnalyzer Analyzer { get; set; }

		[SetUp]
		public void SetUp()
		{
			Analyzer = new PixelAnalyzer();
		}
	}

	[TestFixture]
	internal class GetToneTests : PixelAnalyzerTests
	{
		[Test]
		public void GivenColorWith0000_ReturnsFrequencyAround80()
		{
			var result = Analyzer.GetTone(Color.FromArgb(0, 0, 0, 0));

			Assert.That(result.Frequency, Is.InRange(79, 81));
		}

		[Test]
		public void GivenColorWith0000_ReturnsDurationAroundAQuarter()
		{
			var result = Analyzer.GetTone(Color.FromArgb(0, 0, 0, 0));

			Assert.That(result.Duration, Is.EqualTo(0.25));
		}

		[Test]
		public void GivenRed_ReturnsFrequencyAround80()
		{
			var result = Analyzer.GetTone(Color.Red);

			Assert.That(result.Frequency, Is.InRange(79, 81));
		}

		[Test]
		public void GivenRed_ReturnsDurationOf1()
		{
			var result = Analyzer.GetTone(Color.Red);

			Assert.That(result.Duration, Is.EqualTo(1));
		}

		[Test]
		public void GivenGreen_ReturnsFrequencyAround197()
		{
			var result = Analyzer.GetTone(Color.FromArgb(255, 0, 255, 0));

			Assert.That(result.Frequency, Is.InRange(197, 198));
		}

		[Test]
		public void GivenTeal_ReturnsFrequencyAround309()
		{
			var result = Analyzer.GetTone(Color.FromArgb(255, 0, 250, 250));

			Assert.That(result.Frequency, Is.InRange(308, 310));
		}

		[Test]
		public void GivenTeal_ReturnsDurationAroundPoint97()
		{
			var result = Analyzer.GetTone(Color.FromArgb(255, 0, 250, 250));

			Assert.That(result.Duration, Is.InRange(0.97, 0.98));
		}

		[Test]
		public void GivenGrey_ReturnsVolumePoint5()
		{
			var result = Analyzer.GetTone(Color.Gray);

			Assert.That(result.Volume, Is.EqualTo(Tone.MinimumVolume));
		}

		[Test]
		public void GivenRed255_ReturnsVolume1()
		{
			var result = Analyzer.GetTone(Color.Red);

			Assert.That(result.Volume, Is.EqualTo(1));
		}

		[Test]
		public void GivenRed191_ReturnsVolumeAroundPoint75()
		{
			var result = Analyzer.GetTone(Color.FromArgb(191, 64, 64));

			Assert.That(result.Volume, Is.InRange(.749, .751));
		}
	}

	[TestFixture]
	internal class ConvertToFrequencyTests : PixelAnalyzerTests
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

	[TestFixture]
	class ConvertToDurationTests : PixelAnalyzerTests
	{
		[Test]
		public void Given0_ReturnsDurationMinimum()
		{
			var result = Analyzer.ConvertToDuration(0);

			Assert.That(result, Is.EqualTo(Tone.MinimumDuration));
		}

		[Test]
		public void Given1_ReturnsDurationMax()
		{
			var result = Analyzer.ConvertToDuration(1);

			Assert.That(result, Is.EqualTo(Tone.MaximumDuration));
		}

		[Test]
		public void GivenPoint5_ReturnsDuration2Point5()
		{
			var result = Analyzer.ConvertToDuration((float)0.5);

			Assert.That(result, Is.EqualTo(1));
		}
	}

	[TestFixture]
	class ConvertToVolume : PixelAnalyzerTests
	{
		[Test]
		public void Given0_ReturnsVolumeMinimum()
		{
			var result = Analyzer.ConvertToVolume(0);

			Assert.That(result, Is.EqualTo(Tone.MinimumVolume));
		}

		[Test]
		public void Given1_ReturnsVolumeMax()
		{
			var result = Analyzer.ConvertToVolume(1);

			Assert.That(result, Is.EqualTo(Tone.MaximumVolume));
		}

		[Test]
		public void GivenPoint5_ReturnsDuration7500()
		{
			var result = Analyzer.ConvertToVolume((float)0.5);

			Assert.That(result, Is.EqualTo(0.75));
		}
	}
}
