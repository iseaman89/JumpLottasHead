using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Audios
{
    public class AudioPlayer
    {
        private readonly List<AudioClip> _playlist;
        private readonly AudioSource _audioSource;

        public AudioPlayer(AudioPlaylist playlist, AudioSource audioSource)
        {
            _playlist = playlist.Playlist;
            _audioSource = audioSource;
        }
        
        private AudioClip GetSound(string clipName)
        {
            return _playlist.FirstOrDefault(sound => sound.name == clipName);
        }

        public void PlaySound(string clipName)
        {
            _audioSource.PlayOneShot(GetSound(clipName));
        }

        public void PlayMusic(string clipName)
        {
            _audioSource.clip = GetSound(clipName);
            _audioSource.loop = true;
            _audioSource.Play();
        }

        public void StopMusic() => _audioSource.Stop();
        
        public void ToggleSound(bool isOn)
        {
            AudioListener.volume = isOn ? 1f : 0f;
            PlayerPrefs.SetInt("SoundEnabled", isOn ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
}