using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter
{
    //ITarget Interface
    public interface IMediaPlayer
    {
        void Play(string FileType, string FileName);
    }

    //interface
    public interface IAdvancedMediaPlayer
    {
        void PlayVLC(string FileName);
        void PlayMP4(string FileName);
    }

    //concrete classes implementing the AdvancedMediaPlayer interface
    public class VLCPlayer : IAdvancedMediaPlayer
    {
        public void PlayVLC(string FileName)
        {
            Console.WriteLine("Playing vlc: " + FileName);
        }

        public void PlayMP4(string FileName) { }
    }

    public class MP4Player : IAdvancedMediaPlayer
    {
        public void PlayVLC(string FileName) { }

        public void PlayMP4(string FileName)
        {
            Console.WriteLine("Playing mp4: " + FileName);
        }
    }

    //Adapater Class
    public class MediaAdapter : IMediaPlayer
    {
        private IAdvancedMediaPlayer advancedMediaPlayer;

        public MediaAdapter(string FileType)
        {
            if (FileType == "mp4") advancedMediaPlayer = new MP4Player();
            if (FileType == "vlc") advancedMediaPlayer = new VLCPlayer();
        }

        public void Play(string FileType, string FileName)
        {
            if (FileType == "mp4") advancedMediaPlayer.PlayMP4(FileName);
            if (FileType == "vlc") advancedMediaPlayer.PlayVLC(FileName);
        }
    }

    //The 'Adaptee', Concreate Class implementing the IMediaPlayer interface
    //AudioPlayer uses the adapter class, passing the audio type,
    //without knowing the actual class which can play the desired format.
    public class AudioPlayer : IMediaPlayer
    {
        private MediaAdapter mediaAdapter;
        public void Play(string FileType, string FileName)
        {
            if (FileType == "mp3") Console.WriteLine("Playing mp3: " + FileName);
            else if (FileType == "mp4" || FileType == "vlc")
            {
                mediaAdapter = new MediaAdapter(FileType);
                mediaAdapter.Play(FileType, FileName);
            }
            else
            {
                Console.WriteLine("Invalid File Type");
            }
        }
    }

    public class Client
    {
        public static void Demo()
        {
            AudioPlayer audioPlayer = new AudioPlayer();

            audioPlayer.Play("mp3", "MP3 File");
            audioPlayer.Play("mp4", "MP4 File");
            audioPlayer.Play("vlc", "VLC File");
            audioPlayer.Play("avi", "AVI File");

            Console.ReadLine();
        }
    }
}
