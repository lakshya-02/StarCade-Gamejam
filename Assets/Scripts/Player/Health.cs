using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private float deathRestartDelay = 1f;
    private SpriteRenderer spriteRenderer;
    private bool isDead = false;
    [SerializeField] private AudioClip deathAudio;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage()
    {
        if (isDead) return;
        
        Die();
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Player died! Restarting level...");
        SoundManager.instance.Playsound(deathAudio);
        
        // Notify GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PlayerDied();
        }
        
        // Disable player controls
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
        
        // Visual feedback
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }
        
        // Restart scene after delay
        Invoke("RestartLevel", deathRestartDelay);
    }
    
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
