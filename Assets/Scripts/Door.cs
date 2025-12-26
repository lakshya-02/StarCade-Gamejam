using UnityEngine;

public class Door : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }
    
    void Update()
    {
        if (playerMovement.jumpCounter >= 1)
        {
            spriteRenderer.enabled = true;
            boxCollider.enabled = true;
        }
        else
        {
            spriteRenderer.enabled = false;
            boxCollider.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && playerMovement.jumpCounter >= 1)
        {
            Debug.Log("Level Complete!");
        }
    }
}
