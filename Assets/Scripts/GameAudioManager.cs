using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// GameAudioManager, Responsible for Game Audio
/// </summary>
public class GameAudioManager : Singleton<GameAudioManager>
{
    [SerializeField] AudioSource _sfxAudioSource;
    [SerializeField] List<GameAudio> _gameAudios = new List<GameAudio>();


    /// <summary>
    /// Get Audio Clip
    /// </summary>
    /// <param name="clipName"></param>
    /// <returns></returns>
    AudioClip GetAudioClip(string clipName)
    {
        if (_gameAudios.Count == 0)
            return null;


       var audioClip =  _gameAudios.Find((match) => match._audioKey.Equals(clipName));

        return audioClip._audioClip;
    }

    /// <summary>
    /// Play SFX for particular by Sound Key
    /// </summary>
    /// <param name="soundKey"></param>
    public void PlaySFX(string soundKey)
    {
        var audioClip = GetAudioClip(soundKey);


        if (_sfxAudioSource.isPlaying)
            _sfxAudioSource.Stop();

        _sfxAudioSource.PlayOneShot(audioClip);

    }

    /// <summary>
    /// Stop SFX Sound
    /// </summary>
    public void StopSFX()
    {
        if (_sfxAudioSource.isPlaying)
            _sfxAudioSource.Stop();
    }


}

/// <summary>
/// Game Audio Storage Class
/// </summary>
[Serializable]
public class GameAudio
{
    public string _audioKey;
    public AudioClip _audioClip;

}

