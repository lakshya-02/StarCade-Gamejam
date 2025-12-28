using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    private Collider2D spikeCollider;
    private Health health;

    private void Awake()
    {
        spikeCollider = GetComponent<Collider2D>();
        health = FindAnyObjectByType<Health>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            health.TakeDamage();
        }
    }
}