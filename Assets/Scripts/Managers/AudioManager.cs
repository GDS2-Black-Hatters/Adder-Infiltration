using System.Collections.Generic;
using UnityEngine;

public sealed class AudioManager : MonoBehaviour, IManager
{
    [System.Serializable]
    private class AudioTrack
    {
        public string name;
        public AudioClip clip;
    }

    /// <summary>
    /// Sound effects, music, does not matter as you call the clip via its name.
    /// </summary>
    [SerializeField] private AudioTrack[] audioTracks; //The audio clips to be added via UnityEditor
    private readonly Dictionary<string, AudioClip> allClips = new(); //A dictionary of all the clips that can be called during runtime.

    private AudioSource musicPlayer; //An audio source that plays only music.
    private readonly List<AudioSource> sfxPlayers = new(); //A list of references of audio sources for sound effects.

    public void StartUp()
    {
        foreach (AudioTrack track in audioTracks)
        {
            allClips.Add(track.name, track.clip);
        }
        musicPlayer = gameObject.AddComponent<AudioSource>();
        musicPlayer.loop = true;
    }

    private void PlayAudio(AudioSource source, string clipName)
    {
        if (!allClips.ContainsKey(clipName))
        {
            return;
        }

        source.Stop();
        source.clip = allClips[clipName];
        source.Play();
    }

    /// <summary>
    /// Plays an audio file. It automatically loops. Only one can be played at a time.
    /// </summary>
    /// <param name="clipName">The name of the music file.</param>
    public void PlayMusic(string clipName)
    {
        if (!musicPlayer.clip || !musicPlayer.clip.name.Equals(clipName) || !musicPlayer.isPlaying)
        {
            PlayAudio(musicPlayer, clipName);
        }
    }

    /// <summary>
    /// Plays a audio file. It does not loop. Multiple can be played at a time.
    /// </summary>
    /// <param name="clipName">The name of the music file.</param>
    public void PlaySound(string clipName)
    {
        AudioSource player = null;
        foreach (AudioSource source in sfxPlayers)
        {
            if (source && !source.isPlaying)
            {
                player = source;
                break;
            }
        }

        if (player == null)
        {
            player = gameObject.AddComponent<AudioSource>();
            player.loop = false;
            sfxPlayers.Add(player);
        }

        PlayAudio(player, clipName);
    }

    /// <summary>
    /// Plays an audio clip from an object that is not at the camera.
    /// </summary>
    /// <param name="clipName">The audio clip</param>
    /// <param name="source">The source where the sound is played.</param>
    public void PlaySound(string clipName, AudioSource source)
    {
        if (!source.clip || !source.isPlaying)
        {
            PlayAudio(source, clipName);
        }
    }

    /// <summary>
    /// Stops the current music playing.
    /// </summary>
    public void StopMusic()
    {
        musicPlayer.Stop();
    }
}
