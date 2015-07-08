using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using HearThePicture.Models;
using HearThePicture.Repositories;
using Microsoft.WindowsAzure.Storage.Blob;

namespace HearThePicture.Services
{
	public class WavService
	{
		private readonly MusicBlobRepository _repo;
		private bool UseSynth { get; set; }

		public WavService()
		{
			_repo = new MusicBlobRepository();
		}

		public void Play(List<Tone> tones, int samplesPerPixel, bool synth = true)
		{
			var outFileName = Guid.NewGuid().ToString("N").Substring(5);

			var filePath = string.Format("C:\\Dev\\Wavs\\{0}.wav", outFileName);

			Create(tones, filePath, samplesPerPixel, synth);

			var stream = File.Open(filePath, FileMode.Open);

			stream.Seek(0, 0);

			var player = new SoundPlayer(stream);

			player.Play();
		}

		public void PlayBlob(List<Tone> tones, int samplesPerPixel, bool synth = true)
		{
			var fileId = Guid.NewGuid().ToString("N");

			var reference = CreateBlob(tones, fileId, samplesPerPixel, synth);

			var stream = _repo.Get(reference);

			stream.Seek(0, 0);

			var player = new SoundPlayer(stream);

			player.Play();
		}

		public FileStream Create(List<Tone> tones, string filePath, int baseSamplesPerPixel = 88200, bool synth = true)
		{
			UseSynth = synth;
			var samples = GetTotalSamples(baseSamplesPerPixel, tones);

			FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
			WriteWaves(tones, baseSamplesPerPixel, stream, samples);
			stream.Close();

			return stream;
		}

		public string CreateBlob(List<Tone> tones, string fileId, int baseSamplesPerPixel = 88200, bool synth = true)
		{
			UseSynth = synth;
			var samples = GetTotalSamples(baseSamplesPerPixel, tones);

			string blobReference;
			CloudBlobStream stream = _repo.GetStream(fileId, out blobReference);
			WriteWaves(tones, baseSamplesPerPixel, stream, samples);
			stream.Close();

			return blobReference;
		}

		private void WriteWaves(List<Tone> tones, int baseSamplesPerPixel, Stream stream, int samples)
		{
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
				var samplesForThisTone = baseSamplesPerPixel * tones[i].Duration;
				var amplitudeForThisTone = baseAmplitude * tones[i].Volume;

				var fadePeriod = samplesForThisTone / 15;

				int j = 0;

				for (; j < fadePeriod; j++)
				{
					var fadeAmplitude = j / fadePeriod * amplitudeForThisTone;
					double t = (double)j / (double)samplesPerSecond;
					short waveForm = GetWaveform(fadeAmplitude, t, tones[i].Frequency);
					writer.Write(waveForm);
				}

				for (; j <= samplesForThisTone - fadePeriod; j++)
				{
					double t = (double)j / (double)samplesPerSecond;
					short waveForm = GetWaveform(amplitudeForThisTone, t, tones[i].Frequency);
					writer.Write(waveForm);
				}

				for (; j < samplesForThisTone; j++)
				{
					double fadeAmplitude = ((samplesForThisTone - j) / fadePeriod) * amplitudeForThisTone;
					double t = (double)j / (double)samplesPerSecond;
					short waveForm = GetWaveform(fadeAmplitude, t, tones[i].Frequency);
					writer.Write(waveForm);
				}
			}
			writer.Close();
		}

		private short GetWaveform(double amplitude, double timePosition, double frequency)
		{
			short baseNote = (short)(amplitude * (Math.Sin(timePosition * frequency * 2.0 * Math.PI)));

			if (!UseSynth)
				return baseNote;

			var note = ApplyOctaves(amplitude, timePosition, frequency, baseNote);

			note = ApplySquareWave(amplitude, timePosition, frequency, note);

			note = ApplySawtoothWave(amplitude, timePosition, frequency, note);

			return note;
		}

		private short ApplySawtoothWave(double amplitude, double timePosition, double frequency, short baseNote)
		{
			var weight = 2;

			var initialValue = ((Math.Abs((timePosition % (1 / frequency))) - (1 / (2 * frequency))) * amplitude);

			var sawTooth = (initialValue - initialValue / 2) * weight;

			var note = baseNote + sawTooth;

			return (short)note;
		}

		private short ApplySquareWave(double amplitude, double timePosition, double frequency, short baseNote)
		{
			var weight = 0.025;

			var squareWave = (timePosition % (1 / frequency)) < 1 / (2 * frequency) ? amplitude * weight : -amplitude * weight;

			var note = baseNote + (short)squareWave;
			return (short)note;
		}

		private short ApplyOctaves(double amplitude, double timePosition, double baseFrequency, short baseNote)
		{
			short halfTave = (short)(amplitude / 1000 * (Math.Sin(timePosition * (baseFrequency * 0.5) * 2.0 * Math.PI)));

			short octave = (short)(amplitude / 10 * (Math.Sin(timePosition * (baseFrequency * 2.0) * 2.0 * Math.PI)));

			short secondTave = (short)(amplitude / 100 * (Math.Sin(timePosition * (baseFrequency * 2.0 * 2.0) * 2.0 * Math.PI)));

			short thirdTave = (short)(amplitude / 1000 * (Math.Sin(timePosition * (baseFrequency * 2.0 * 2.0 * 2.0) * 2.0 * Math.PI)));

			return (short)(baseNote + halfTave + octave + secondTave + thirdTave);
		}

		private int GetTotalSamples(int baseSamplesPerPixel, List<Tone> tones)
		{
			double samples = tones.Sum(tone => tone.Duration * baseSamplesPerPixel);

			return (int)Math.Ceiling(samples);
		}
	}
}