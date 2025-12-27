using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VolumeController : MonoBehaviour
{
    [Header("Sliders")]
    public Slider masterSlider;
    public Slider musicSlider;

    private AudioSource[] musicSources;

    void Start()
    {
        // Default values
        if (!PlayerPrefs.HasKey("MasterVolume"))
            PlayerPrefs.SetFloat("MasterVolume", 1f);

        if (!PlayerPrefs.HasKey("MusicVolume"))
            PlayerPrefs.SetFloat("MusicVolume", 1f);

        // Load saved values
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");

        ApplyMasterVolume(masterSlider.value);
        ApplyMusicVolume(musicSlider.value);

        // Slider listeners
        masterSlider.onValueChanged.AddListener(ApplyMasterVolume);
        musicSlider.onValueChanged.AddListener(ApplyMusicVolume);

        // Find music sources
        FindMusicSources();
    }

    void FindMusicSources()
    {
        GameObject[] musicObjects = GameObject.FindGameObjectsWithTag("Music");
        musicSources = new AudioSource[musicObjects.Length];

        for (int i = 0; i < musicObjects.Length; i++)
        {
            musicSources[i] = musicObjects[i].GetComponent<AudioSource>();
        }
    }

    void ApplyMasterVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("MasterVolume", value);
    }

    void ApplyMusicVolume(float value)
    {
        if (musicSources == null) return;

        foreach (AudioSource source in musicSources)
        {
            if (source != null)
                source.volume = value;
        }

        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    // Handles new scenes (important)
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindMusicSources();
        ApplyMusicVolume(musicSlider.value);
    }
}
