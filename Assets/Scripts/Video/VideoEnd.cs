using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class VideoFadeOutCanvas : MonoBehaviour
{
    [Header("References")]
    public VideoPlayer videoPlayer;
    public CanvasGroup canvasGroup;

    [Header("Fade Settings")]
    public float fadeDuration = 1.5f;

    void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd;
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float startAlpha = canvasGroup.alpha;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, time / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        gameObject.SetActive(false); // disables Canvas
    }
}
