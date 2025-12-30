using UnityEngine;
using UnityEngine.SceneManagement;

public class Door2 : MonoBehaviour
{
    [Header("Unlock Settings")]
    [SerializeField] private int requiredJumps = 5;
    [SerializeField] private Sprite lockedSprite;
    [SerializeField] private Sprite unlockedSprite;

    [Header("Detection Settings")]
    [SerializeField] private float detectionRange = 3f;
    [SerializeField] private string playerTag = "Player";
    
    private PlayerMovement playerMovement;
    private SpriteRenderer spriteRenderer;
    private bool isUnlocked = false;
    private Transform playerTransform;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        
        if (lockedSprite != null)
        {
            spriteRenderer.sprite = lockedSprite;
        }
    }
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        
        if (distanceToPlayer <= detectionRange)
        {
            if (isUnlocked)
            {
                SceneManager.LoadScene(2);
                spriteRenderer.enabled = false;
            }
            else
            {
                spriteRenderer.enabled = true;
            }
        }

        if (!isUnlocked && playerMovement != null && playerMovement.jumpCounter >= requiredJumps)
        {
            UnlockDoor();
        }
    }
    
    private void UnlockDoor()
    {
        isUnlocked = true;
        if (unlockedSprite != null)
        {
            spriteRenderer.sprite = unlockedSprite;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
