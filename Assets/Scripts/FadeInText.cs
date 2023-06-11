using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInText : MonoBehaviour
{
    //[SerializeField] private float fadeDuration = 1f;
    //[SerializeField] private float scaleDuration = 1f;

    private Text text;
    //private CanvasGroup canvasGroup;
    const float maxTime = 2.0f;
    public float currentTime = 0.0f;

    private void Start()
    {
        //canvasGroup = GetComponent<CanvasGroup>();

        //StartCoroutine(StartFadeIn());
    }

    void Update()
    {
        //StartCoroutine(Fade());
        currentTime += Time.deltaTime; //que cada frame sume tiempo
        if (currentTime >= maxTime)
        {
            StartCoroutine(FadeIn());

            Debug.Log("PATO");
            currentTime = 0;
        }
    }

    IEnumerator Fade() //Fade out
    {
        for (float i = 1.0f; i >= 0f; i -= 0.01f) //alpha 1 es totalmente opaco, alpha 0 es transparente
        {
            Color c = text.color;
            c.a = i; //a = alpha, i es el que está decrementando
            text.color = c;
            yield return null; //yield para poder devolver un IEnumerator
                               //yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator FadeIn()
    {
        for (float i = 0.0f; i <= 0f; i = 1.0f) //alpha 1 es totalmente opaco, alpha 0 es transparente
        {
            Color c = text.color;
            c.a = i; //a = alpha, i es el que está decrementando
            text.color = c;
            yield return null;
        }
    }

    //private IEnumerator StartFadeIn()
    //{
    //    canvasGroup.alpha = 0f;
    //    canvasGroup.blocksRaycasts = false;

    //    // Fade-in
    //    float elapsedTime = 0f;
    //    while (elapsedTime < fadeDuration)
    //    {
    //        float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
    //        canvasGroup.alpha = alpha;
    //        elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }
    //    canvasGroup.alpha = 1f;

    //    // Scale-in
    //    Vector3 originalScale = transform.localScale;
    //    float scaleElapsedTime = 0f;
    //    while (scaleElapsedTime < scaleDuration)
    //    {
    //        float scale = Mathf.Lerp(0.5f, 1f, scaleElapsedTime / scaleDuration);
    //        transform.localScale = originalScale * scale;
    //        scaleElapsedTime += Time.deltaTime;
    //        yield return null;
    //    }
    //    transform.localScale = originalScale;

    //    // Fade-out
    //    yield return new WaitForSeconds(fadeDuration);
    //    elapsedTime = 0f;
    //    while (elapsedTime < fadeDuration)
    //    {
    //        float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
    //        canvasGroup.alpha = alpha;
    //        elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }
    //    canvasGroup.alpha = 0f;

    //    // Disable text object
    //    canvasGroup.blocksRaycasts = false;
    //    gameObject.SetActive(false);
    //}
}