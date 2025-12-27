using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject optionsMenu;

    [Header("Scene")]
    public string gameSceneName = "Game";

    [Header("Button Sound")]
    public AudioSource uiAudioSource;
    public AudioClip buttonClickSound;

    // PLAY BUTTON
    public void PlayGame()
    {
        PlayClickSound();
        SceneManager.LoadScene(gameSceneName);
    }

    // OPTIONS BUTTON
    public void OpenOptions()
    {
        PlayClickSound();
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    // BACK BUTTON
    public void BackToMainMenu()
    {
        PlayClickSound();
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    // QUIT BUTTON
    public void QuitGame()
    {
        PlayClickSound();
        Application.Quit();
        Debug.Log("Game Quit");
    }

    // --- SOUND ---
    void PlayClickSound()
    {
        if (uiAudioSource != null && buttonClickSound != null)
        {
            uiAudioSource.PlayOneShot(buttonClickSound);
        }
    }
}
