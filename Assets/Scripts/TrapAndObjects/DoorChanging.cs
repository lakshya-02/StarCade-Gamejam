using UnityEngine;

public class DoorChanging : MonoBehaviour
{
    [Header("Door Objects")]
    [SerializeField] private GameObject currentDoor;
    [SerializeField] private GameObject newDoor;
    
    [Header("Detection Settings")]
    [SerializeField] private float detectionRange = 3f;
    [SerializeField] private string playerTag = "Player";
    
    [Header("Optional Settings")]
    [SerializeField] private bool changeOnlyOnce = true;
    
    private bool hasChanged = false;
    private Transform playerTransform;
    
    void Start()
    {
        // Find player in scene
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        if (player != null)
        {
            playerTransform = player.transform;
        }
        
        // Ensure new door is initially inactive
        if (newDoor != null)
        {
            newDoor.SetActive(false);
        }
    }
    
    void Update()
    {
        // Check if door has already changed and changeOnlyOnce is enabled
        if (hasChanged && changeOnlyOnce)
            return;
        
        // Check if player exists
        if (playerTransform == null)
            return;
        
        // Calculate distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        
        // If player is within detection range, change the door
        if (distanceToPlayer <= detectionRange)
        {
            ChangeDoor();
        }
    }
    
    private void ChangeDoor()
    {
        if (currentDoor != null)
        {
            currentDoor.SetActive(false);
        }
        
        if (newDoor != null)
        {
            newDoor.SetActive(true);
        }
        
        hasChanged = true;
    }
    
    // Visualize detection range in editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
