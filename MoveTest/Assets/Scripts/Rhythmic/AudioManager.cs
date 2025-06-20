using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Volume")]
    [Range(0, 1)] public float masterVolume;
    [Range(0, 1)] public float musicVolume;
    [Range(0, 1)] public float sfxVolume;

    private Bus masterBus;
    private Bus musicBus;
    private Bus sfxBus;
    private List<EventInstance> eventInstances;
    public static AudioManager instance { get; private set; }

    public EventInstance musicEventInstance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Hay mas de 1 audio manager en escena");
        }
        instance = this;

        eventInstances = new List<EventInstance>();

        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");
        InitializeSong(FMODEvents.instance.song);
    }

    private void Start()
    {
        //InitializeSong(FMODEvents.instance.song);
    }

    private void Update()
    {
        masterBus.setVolume(masterVolume);
        musicBus.setVolume(musicVolume);
        sfxBus.setVolume(sfxVolume);
        Debug.Log("Handle: " + musicEventInstance.handle);
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    private void InitializeSong(EventReference musicEventReference)
    {
        musicEventInstance = CreateInstance(musicEventReference);
        musicEventInstance.start();
    }

    public void PauseMusic()
    {
        musicEventInstance.setPaused(true);
    }

    public void ResumeMusic()
    {
        musicEventInstance.setPaused(false);
    }

    public EventInstance GetMusicEventInstance()
    {
    return musicEventInstance;
    }

    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

}
    