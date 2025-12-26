using UnityEngine;
using UnityEngine.UIElements;

public class Door : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    [SerializeField] private Sprite newsprite;
    
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
            spriteRenderer.sprite = newsprite;
        }
        else
        {
            spriteRenderer.enabled = true;
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
