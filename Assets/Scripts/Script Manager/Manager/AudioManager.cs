using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;
    public AudioMixerGroup mixerGroup;

    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    public bool playOnAwake;

    [HideInInspector]
    public AudioSource source;

}

[System.Serializable]
public class SoundData
{
    public string audioFileName;
    public float volume;
    public float musicVolume;
    public float effectsVolume;
}

[System.Serializable]
public class SoundList
{
    public List<SoundData> sounds;
}

public class AudioManager : SingletonPersistent<AudioManager>
{
    public Sound[] sounds;

    private void Start()
    {
        OnSoundSettingsSaved();

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
            s.source.outputAudioMixerGroup = s.mixerGroup;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }

        s.source.Play();
    }

    public void OnVolumeChanged()
    {
        SoundData soundData = new SoundData();
        soundData.audioFileName = "mi_audio.mp3";
        soundData.volume = 1.0f;
        soundData.musicVolume = OptionsManager.Instance.musicVolumeSlider.value;
        soundData.effectsVolume = OptionsManager.Instance.sfxVolumeSlider.value;

        SoundList soundList = new SoundList();
        soundList.sounds = new List<SoundData>();
        soundList.sounds.Add(soundData);

        string json = JsonUtility.ToJson(soundList);
        PlayerPrefs.SetString("SoundData", json);
    }

    public void OnSoundSettingsSaved()
    {
        SoundList soundList = JsonUtility.FromJson<SoundList>(PlayerPrefs.GetString("SoundData"));
        string audioFileName = soundList.sounds[0].audioFileName;
        float volume = soundList.sounds[0].volume;
        float musicVolume = soundList.sounds[0].musicVolume;
        float effectsVolume = soundList.sounds[0].effectsVolume;

        OptionsManager.Instance.musicVolumeSlider.value = musicVolume;
        OptionsManager.Instance.sfxVolumeSlider.value = effectsVolume;
    }

}
