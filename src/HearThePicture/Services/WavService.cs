using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using HearThePicture.Models;

namespace HearThePicture.Services
{
	public class WavService
	{
		public void Play(List<Tone> tones, int samplesPerPixel)
		{
			var outFileName = Guid.NewGuid().ToString("N").Substring(5);

			var filePath = string.Format("C:\\Dev\\Wavs\\{0}.wav", outFileName);

			Create(tones, filePath, samplesPerPixel);

			var stream = File.Open(filePath, FileMode.Open);

			stream.Seek(0, 0);

			var player = new SoundPlayer(stream);

			player.Play();
		}

		public FileStream Create(List<Tone> tones, string filePath, int baseSamplesPerPixel = 88200)
		{
			var samples = GetTotalSamples(baseSamplesPerPixel, tones);

			FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
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
			double baseAmplitude = 10000;

			for (int i = 0; i < tones.Count; i++)
			{
				var samplesForThisTone = baseSamplesPerPixel*tones[i].Duration;
				var amplitude = baseAmplitude*tones[i].Volume;

				var fadePeriod = samplesForThisTone/15;

				int j = 0;

				for (; j < fadePeriod; j++)
				{
					var fadeAmplitude = j / fadePeriod * amplitude;
					double t = (double)j / (double)samplesPerSecond;
					short s = (short)(fadeAmplitude * (Math.Sin(t * tones[i].Frequency * 2.0 * Math.PI)));
					writer.Write(s);
				}

				for (; j <= samplesForThisTone - fadePeriod; j++)
				{
					double t = (double)j / (double)samplesPerSecond;
					short s = (short)(amplitude * (Math.Sin(t * tones[i].Frequency * 2.0 * Math.PI)));
					writer.Write(s);
				}

				for (; j < samplesForThisTone; j++)
				{
					var fadeAmplitude = ((samplesForThisTone - j) / fadePeriod) * amplitude;
					double t = (double)j / (double)samplesPerSecond;
					short s = (short)(fadeAmplitude * (Math.Sin(t * tones[i].Frequency * 2.0 * Math.PI)));
					writer.Write(s);
				}
			}
			writer.Close();
			stream.Close();

			return stream;
		}

		private int GetTotalSamples(int baseSamplesPerPixel, List<Tone> tones)
		{
			double samples = tones.Sum(tone => tone.Duration*baseSamplesPerPixel);

			return (int) Math.Ceiling(samples);
		}
	}
}