using NAudio.Wave;
using System.Collections.Generic;
using System.IO;

namespace MeuLeeDiaPlayer.Common.Models
{
    public class Song
    {
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
        
        private string _songName;
        private string _artistName;

        public Song(IAudioStream audioStream, string songName, string artistName)
            => (FileReader, SongName, ArtistName) = (audioStream.Stream, songName, artistName);


        // this method is big time slow. maybe parallel could help
        public static IEnumerable<Song> GetSongsFromFolder(string folder, bool normalizeVolume = true)
        {
            foreach (var songPath in Utils.GetAudioOnlyFilesInFolder(folder))
            {
                yield return new Song(new AudioStream(songPath, normalizeVolume), Path.GetFileNameWithoutExtension(songPath), null);
            }
        }

        public override string ToString()
        {
            return $"{SongName} - {ArtistName}";
        }
    }
}
