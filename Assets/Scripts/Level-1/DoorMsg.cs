using UnityEngine;
using System.Collections;

public class DoorTrigger2D : MonoBehaviour
{
    public GameObject doorMsg;
    public float messageDuration = 3f;
    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(ShowMessage());
        }
    }

    IEnumerator ShowMessage()
    {
        doorMsg.SetActive(true);
        yield return new WaitForSeconds(messageDuration);
        doorMsg.SetActive(false);
        gameObject.SetActive(false);
    }
}
