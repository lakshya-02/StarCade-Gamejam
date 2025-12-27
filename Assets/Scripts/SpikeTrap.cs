using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    private Collider2D spikeCollider;

    private void Awake()
    {
        spikeCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player hit spike trap!");
        }
    }
}