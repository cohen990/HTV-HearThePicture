using System.Collections.Generic;
using System.IO;
using System.Media;
using HearThePicture.Models;
using HearThePicture.Services;
using NUnit.Framework;

namespace HearThePicture.Tests.Services
{
	class WavServiceTests
	{
		public WavService Wav { get; set; }
		internal const string FileName = "test.wav";

		[SetUp]
		public void SetUp()
		{
			Wav = new WavService();
		}

		[TearDown]
		public void TearDown()
		{
			File.Delete(FileName);
		}
	}

	[TestFixture]
	class CreateTests : WavServiceTests
	{
		[Test]
		public void WritesFile()
		{
			var tones = new List<Tone> {new Tone{ Frequency = 220.0 } };
			Wav.Create(tones, FileName);

			Assert.That(File.Exists(FileName), Is.True);
		}

		[Test]
		[Ignore("Read Create permission confliction")]
		public void WritesFileThatCanBePlayed()
		{
			var tones = new List<Tone> { new Tone { Frequency = 220.0 } };

			Wav.Create(tones, FileName);

			var stream = File.Open(FileName, FileMode.Open, FileAccess.Read, FileShare.Delete);

			var player = new SoundPlayer(stream);

			Assert.DoesNotThrow(() => player.Play());
		}

		[Test]
		[Ignore("Read Create permission confliction")]
		public void WritesFileWithDifferentFrequencies()
		{
			var tones = new List<Tone> { new Tone { Frequency = 680.0 } };

			Wav.Create(tones, FileName);

			var stream = File.Open(FileName, FileMode.Open, FileAccess.Read, FileShare.Delete);

			var player = new SoundPlayer(stream);

			Assert.DoesNotThrow(() => player.Play());
		}

		[Test]
		[Ignore("Read Create permission confliction")]
		public void WritesFileWhenGivenMultipleFrequencies()
		{
			var tones = new List<Tone> { new Tone { Frequency = 680.0 }, new Tone { Frequency = 230.0 } };

			Wav.Create(tones, FileName);

			var stream = File.Open(FileName, FileMode.Open, FileAccess.Read, FileShare.Delete);

			var player = new SoundPlayer(stream);

			Assert.DoesNotThrow(() => player.Play());
		}
	}
}
