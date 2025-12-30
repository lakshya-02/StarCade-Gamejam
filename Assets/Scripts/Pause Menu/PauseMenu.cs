using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI")]
    public GameObject pauseMenuCanvas;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip buttonClickSound;

    private bool isPaused = false;

    void Start()
    {
        pauseMenuCanvas.SetActive(false);
        ResumeGame(false); // no sound on start
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame(true);
            else
                PauseGame(true);
        }
    }

    void PauseGame(bool playSound)
    {
        if (playSound) PlayClickSound();

        pauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        ResumeGame(true); // called by button
    }

    void ResumeGame(bool playSound)
    {
        if (playSound) PlayClickSound();

        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void LoadMainMenu()
    {
        PlayClickSound();
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    void PlayClickSound()
    {
        if (audioSource != null && buttonClickSound != null)
            audioSource.PlayOneShot(buttonClickSound);
    }
}
