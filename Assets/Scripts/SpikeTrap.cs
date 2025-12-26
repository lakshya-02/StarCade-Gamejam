using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private int damageAmount;
    private Collider2D spikeCollider;

    private void Awake()
    {
        spikeCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health playerHealth = collision.GetComponent<Health>();
        if (playerHealth != null && collision.gameObject.tag == "Player")
        {
            Debug.Log("Player hit spike trap!");
            playerHealth.TakeDamage();
        }
    }
}