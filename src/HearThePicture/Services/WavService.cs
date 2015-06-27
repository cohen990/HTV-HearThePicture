using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;

namespace HearThePicture.Services
{
	public class WavService
	{
		public void Play(List<double> frequencies, int samplesPerPixel)
		{
			var outFileName = Guid.NewGuid().ToString("N").Substring(5);

			Create(frequencies, outFileName + ".wav", samplesPerPixel);

			var filePath = string.Format("C:\\Program Files (x86)\\IIS Express\\{0}.wav", outFileName);

			var stream = File.Open(filePath, FileMode.Open);

			stream.Seek(0,0);

			var player = new SoundPlayer(stream);

			player.Play();
		}

		public FileStream Create(List<double> frequencies, string fileName, int samplesPerPixel = 88200)
		{
			FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
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
			int samples = samplesPerPixel * frequencies.Count;
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
			double ampl = 10000;
			for (int i = 0; i < frequencies.Count; i++)
			{
				for (int j = 0; j < samples / frequencies.Count; j++)
				{
					double t = (double)j / (double)samplesPerSecond;
					short s = (short)(ampl * (Math.Sin(t * frequencies[i] * 2.0 * Math.PI)));
					writer.Write(s);
				}
			}
			writer.Close();
			stream.Close();

			return stream;
		}
	}
}