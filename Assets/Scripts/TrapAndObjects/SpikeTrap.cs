using System.Collections;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    private Collider2D spikeCollider;
    private Health health;
    private Vector3 originalPosition;
    private bool isRetracted = false;

    [SerializeField] private float retractDistance = 1f;
    [SerializeField] private float retractSpeed;
    [SerializeField] private float delayBeforeRise;
    [SerializeField] private float delayBeforeRetract;

    private void Awake()
    {
        spikeCollider = GetComponent<Collider2D>();
        health = FindAnyObjectByType<Health>();
        originalPosition = transform.position;
    }

    private void Start()
    {
        StartCoroutine(AutoRetractCycle());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isRetracted)
        {
            health.TakeDamage();
        }
    }

    private IEnumerator AutoRetractCycle()
    {
        while (true)
        {
            // Wait before retracting
            yield return new WaitForSeconds(delayBeforeRetract);

            yield return StartCoroutine(RetractAndRise());
        }
    }

    private IEnumerator RetractAndRise()
    {
        isRetracted = true;
        spikeCollider.enabled = false;

        // Move down into the ground
        Vector3 targetPosition = originalPosition - new Vector3(0, retractDistance, 0);
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, retractSpeed * Time.deltaTime);
            yield return null;
        }

        // Wait for 1 second
        yield return new WaitForSeconds(delayBeforeRise);

        // Move back up
        while (Vector3.Distance(transform.position, originalPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, retractSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = originalPosition;
        spikeCollider.enabled = true;
        isRetracted = false;
    }
}