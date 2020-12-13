using NAudio.Wave;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MeuLeeDiaPlayer.Common.Models
{
    public class Song
    {
        #region Properties

        public string SongName
        {
            get => _songName;
            set => _songName = value ?? "Unknown Song";
        }

        public string ArtistName
        {
            get => _artistName;
            set => _artistName = value ?? "Unknown Artist";
        }

        public AudioFileReader FileReader { get; private set; }

        #endregion

        private string _songName;
        private string _artistName;
        private static readonly ParallelOptions _options = new ParallelOptions
        {
            MaxDegreeOfParallelism = Environment.ProcessorCount
        };

        public Song(IAudioStream audioStream, string songName, string artistName)
            => (FileReader, SongName, ArtistName) = (audioStream.Stream, songName, artistName);
        
        public static List<Song> GetSongsFromFolder(string folder, bool normalizeVolume = true)
        {
            var songPaths = Utils.GetAudioOnlyFilesInFolder(folder);
            var songs = new ConcurrentBag<Song>();
            Parallel.ForEach(songPaths, _options, songPath =>
            {
                songs.Add(new Song(new AudioStream(songPath, normalizeVolume), Path.GetFileNameWithoutExtension(songPath), null));
            });

            return songs.ToList();
        }

        public override string ToString()
        {
            return $"{SongName} - {ArtistName}";
        }
    }
}
