/*
 * https://github.com/naudio/NAudio/blob/master/NAudio/Mp3FileReader.cs
 * 
 * Credit: Mark Heath
 * Taken cause I can't install the entire NAudio Package
 * as the WinForm package won't build as this project doesn't only target windows
 * will be removed as soon as this is fixed
 */



using NAudio.Wave;
using System.IO;

namespace Kyvos.Audio;

/// <summary>
/// Class for reading from MP3 files
/// </summary>
internal class Mp3FileReader : Mp3FileReaderBase
{
    /// <summary>Supports opening a MP3 file</summary>
    public Mp3FileReader(string mp3FileName)
        : base(File.OpenRead(mp3FileName), CreateAcmFrameDecompressor, true)
    {
    }

    /// <summary>
    /// Opens MP3 from a stream rather than a file
    /// Will not dispose of this stream itself
    /// </summary>
    /// <param name="inputStream">The incoming stream containing MP3 data</param>
    public Mp3FileReader(Stream inputStream)
        : base(inputStream, CreateAcmFrameDecompressor, false)
    {

    }

    /// <summary>
    /// Creates an ACM MP3 Frame decompressor. This is the default with NAudio
    /// </summary>
    /// <param name="mp3Format">A WaveFormat object based </param>
    /// <returns></returns>
    public static IMp3FrameDecompressor CreateAcmFrameDecompressor(WaveFormat mp3Format)
    {
        // new DmoMp3FrameDecompressor(this.Mp3WaveFormat); 
        return new AcmMp3FrameDecompressor(mp3Format);
    }
}