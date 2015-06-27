using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
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
	class SaveTests : WavServiceTests
	{
		[Test]
		public void WritesFile()
		{
			Wav.Create(220.0, FileName);

			Assert.That(File.Exists(FileName), Is.True);
		}

		[Test]
		[Ignore("Read Create permission confliction")]
		public void WritesFileThatCanBePlayed()
		{
			Wav.Create(220.0, FileName);

			var stream = File.Open(FileName, FileMode.Open, FileAccess.Read, FileShare.Delete);

			var player = new SoundPlayer(stream);

			Assert.DoesNotThrow(() => player.Play());
		}

		[Test]
		[Ignore("Read Create permission confliction")]
		public void WritesFileWithDifferentFrequencies()
		{
			Wav.Create(680.0, FileName);

			var stream = File.Open(FileName, FileMode.Open, FileAccess.Read, FileShare.Delete);

			var player = new SoundPlayer(stream);

			Assert.DoesNotThrow(() => player.Play());
		}
	}
}
