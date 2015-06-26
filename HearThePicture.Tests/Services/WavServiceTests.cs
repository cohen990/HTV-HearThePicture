using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace HearThePicture.Tests.Services
{
	class WavServiceTests
	{
		public WavService Wav { get; set; }

		[SetUp]
		public void SetUp()
		{
			Wav = new WavService();
		}

		[TearDown]
		public void TearDown()
		{
			File.Delete("test.wav");
		}
	}

	[TestFixture]
	class SaveTests : WavServiceTests
	{
		[Test]
		public void WritesFile()
		{
			Wav.Write();

			Assert.That(File.Exists("test.wav"), Is.True);
		}

		[Test]
		public void WritesFileThatCanBePlayed()
		{
			Wav.Write();

			var stream = File.Open("test.wav", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

			var player = new SoundPlayer(stream);

			Assert.DoesNotThrow(() => player.Play());
		}
	}

	public class WavService
	{
		public void Write()
		{
			FileStream stream = new FileStream("test.wav", FileMode.Create);
			BinaryWriter writer = new BinaryWriter(stream);
			int RIFF = 0x46464952;
			int WAVE = 0x45564157;
			int formatChunkSize = 16;
			int headerSize = 8;
			int format = 0x20746D66;
			short formatType = 1;
			short tracks = 1;
			int samplesPerSecond = 44100;
			short bitsPerSample = 16;
			short frameSize = (short)(tracks * ((bitsPerSample + 7) / 8));
			int bytesPerSecond = samplesPerSecond * frameSize;
			int waveSize = 4;
			int data = 0x61746164;
			int samples = 88200 * 4;
			int dataChunkSize = samples * frameSize;
			int fileSize = waveSize + headerSize + formatChunkSize + headerSize + dataChunkSize;
			writer.Write(RIFF);
			writer.Write(fileSize);
			writer.Write(WAVE);
			writer.Write(format);
			writer.Write(formatChunkSize);
			writer.Write(formatType);
			writer.Write(tracks);
			writer.Write(samplesPerSecond);
			writer.Write(bytesPerSecond);
			writer.Write(frameSize);
			writer.Write(bitsPerSample);
			writer.Write(data);
			writer.Write(dataChunkSize);
			double aNatural = 220.0;
			double ampl = 10000;
			double perfect = 1.5;
			double concert = 1.498307077;
			double freq = aNatural * perfect;
			for (int i = 0; i < samples / 4; i++)
			{
				double t = (double)i / (double)samplesPerSecond;
				short s = (short)(ampl * (Math.Sin(t * freq * 2.0 * Math.PI)));
				writer.Write(s);
			}
			freq = aNatural * concert;
			for (int i = 0; i < samples / 4; i++)
			{
				double t = (double)i / (double)samplesPerSecond;
				short s = (short)(ampl * (Math.Sin(t * freq * 2.0 * Math.PI)));
				writer.Write(s);
			}
			for (int i = 0; i < samples / 4; i++)
			{
				double t = (double)i / (double)samplesPerSecond;
				short s = (short)(ampl * (Math.Sin(t * freq * 2.0 * Math.PI) + Math.Sin(t * freq * perfect * 2.0 * Math.PI)));
				writer.Write(s);
			}
			for (int i = 0; i < samples / 4; i++)
			{
				double t = (double)i / (double)samplesPerSecond;
				short s = (short)(ampl * (Math.Sin(t * freq * 2.0 * Math.PI) + Math.Sin(t * freq * concert * 2.0 * Math.PI)));
				writer.Write(s);
			}
			writer.Close();
			stream.Close();
		}
	}
}
