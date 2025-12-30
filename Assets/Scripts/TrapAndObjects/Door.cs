using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Door : MonoBehaviour
{
    [Header("Unlock Condition")]
    [SerializeField] private int requiredFlips = 5;

    [Header("Door Sprites")]
    [SerializeField] private Sprite lockedSprite;
    [SerializeField] private Sprite unlockedSprite;

    [Header("End Screen UI")]
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TextMeshProUGUI deathText;

    [Header("Audio")]
    [SerializeField] private AudioSource bgmSource;     // Background music
    [SerializeField] private AudioSource sfxSource;     // Sound effects
    [SerializeField] private AudioClip doorOpenAudio;
    [SerializeField] private AudioClip gameCompleteMusic;

    private PlayerMovement playerMovement;
    private SpriteRenderer spriteRenderer;

    private bool isUnlocked = false;
    private bool playerInRange = false;
    private bool gameEnded = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = FindAnyObjectByType<PlayerMovement>();

        if (lockedSprite != null)
            spriteRenderer.sprite = lockedSprite;

        if (endScreen != null)
            endScreen.SetActive(false);
    }

    private void Update()
    {
        // Unlock door
        if (!isUnlocked && playerMovement != null &&
            playerMovement.flipCounter >= requiredFlips)
        {
            UnlockDoor();
        }

        // End game
        if (isUnlocked && playerInRange && !gameEnded)
        {
            EndGame();
        }

        // ENTER → Main Menu
        if (gameEnded && Input.GetKeyDown(KeyCode.Return))
        {
            LoadMainMenu();
        }
    }

    private void UnlockDoor()
    {
        isUnlocked = true;

        if (unlockedSprite != null)
            spriteRenderer.sprite = unlockedSprite;

        if (sfxSource && doorOpenAudio)
            sfxSource.PlayOneShot(doorOpenAudio);
    }

    private void EndGame()
    {
        gameEnded = true;

        // Pause everything
        Time.timeScale = 0f;

        // Audio
        if (bgmSource)
            bgmSource.Stop();

        if (sfxSource && gameCompleteMusic)
            sfxSource.PlayOneShot(gameCompleteMusic);

        // Show total deaths
        if (deathText != null && GameManager.Instance != null)
        {
            deathText.text = "Total Deaths: " + GameManager.Instance.totalDeaths;
        }

        // Show End Screen
        if (endScreen)
            endScreen.SetActive(true);
    }

    private void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInRange = false;
    }
}
