using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private int requiredJumps = 5;
    [SerializeField] private Sprite lockedSprite;
    [SerializeField] private Sprite unlockedSprite;
    
    private PlayerMovement playerMovement;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private bool isUnlocked = false;
    private bool playerInRange = false;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        
        if (lockedSprite != null)
        {
            spriteRenderer.sprite = lockedSprite;
        }
    }
    
    void Update()
    {
        // Check if player has jumped enough times to unlock
        if (!isUnlocked && playerMovement != null && playerMovement.jumpCounter >= requiredJumps)
        {
            UnlockDoor();
        }
        
        // Allow interaction only if unlocked and player is in range
        if (isUnlocked && playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            InteractWithDoor();
        }
    }
    
    private void UnlockDoor()
    {
        isUnlocked = true;
        if (unlockedSprite != null)
        {
            spriteRenderer.sprite = unlockedSprite;
        }
        Debug.Log("Door unlocked! Press E to interact.");
    }
    
    private void InteractWithDoor()
    {
        Debug.Log("Level Complete!");
        
        // Load next level after a short delay
        Invoke("LoadNextLevel", 1.5f);
    }
    
    private void LoadNextLevel()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadNextLevel();
        }
        else
        {
            Debug.LogWarning("GameManager not found! Add GameManager to scene.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;
            if (!isUnlocked)
            {
                Debug.Log($"Door locked! Jump {requiredJumps - playerMovement.jumpCounter} more times to unlock.");
            }
            else
            {
                Debug.Log("Press E to interact with door.");
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
