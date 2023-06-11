using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInText : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float scaleDuration = 1f;

    private Text textComponent;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        textComponent = GetComponent<Text>();
        canvasGroup = GetComponent<CanvasGroup>();

        StartCoroutine(StartFadeIn());
    }

    private IEnumerator StartFadeIn()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;

        // Fade-in
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            canvasGroup.alpha = alpha;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;

        // Scale-in
        Vector3 originalScale = transform.localScale;
        float scaleElapsedTime = 0f;
        while (scaleElapsedTime < scaleDuration)
        {
            float scale = Mathf.Lerp(0.5f, 1f, scaleElapsedTime / scaleDuration);
            transform.localScale = originalScale * scale;
            scaleElapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale;

        // Fade-out
        yield return new WaitForSeconds(fadeDuration);
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            canvasGroup.alpha = alpha;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;

        // Disable text object
        canvasGroup.blocksRaycasts = false;
        gameObject.SetActive(false);
    }
}