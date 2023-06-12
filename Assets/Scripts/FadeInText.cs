using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class FadeInText : MonoBehaviour
{
    public TMP_Text textElement;
    public float fadeInDuration = 14f;
    public float fadeOutDuration = 30;
    public float delayBetweenPhrases = 10f;
    private bool showDialogue = false;
    public CanvasGroup dialogPanel;

    private void Start()
    {
        gameObject.SetActive(false);
        dialogPanel.alpha = 0f;
    }

    private void Update()
    {
        if (!showDialogue)
        {

            StartCoroutine(PanelFadeIn());
            showDialogue = true;
        }
    }

    public IEnumerator PanelFadeIn()
    {
        //dialogPanel.alpha = 0f;
        gameObject.SetActive(true);
        float elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            dialogPanel.alpha = alpha;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        dialogPanel.alpha = 1f;
        StartCoroutine(ShowPhrases());

    }

    public IEnumerator ShowPhrases()
    {
        textElement.gameObject.SetActive(true);

        yield return StartCoroutine(FadeIn("", fadeInDuration));
        yield return new WaitForSeconds(fadeInDuration + delayBetweenPhrases);
        yield return StartCoroutine(FadeOut(fadeOutDuration));

        yield return StartCoroutine(FadeIn("Lilith", fadeInDuration));
        yield return new WaitForSeconds(fadeInDuration + delayBetweenPhrases);
        yield return StartCoroutine(FadeOut(fadeOutDuration));

        yield return StartCoroutine(FadeIn("Por fin has acudido a mi...", fadeInDuration));
        yield return new WaitForSeconds(fadeInDuration + delayBetweenPhrases);
        yield return StartCoroutine(FadeOut(fadeOutDuration));

         yield return StartCoroutine(FadeIn("La magia ancestral que has despertado es solo el comienzo.", fadeInDuration));
        yield return new WaitForSeconds(fadeInDuration + delayBetweenPhrases);
        yield return StartCoroutine(FadeOut(fadeOutDuration));

        yield return StartCoroutine(FadeIn("Si quieres salvar a tu hermano, debes afrontar tu destino.", fadeInDuration));
        yield return new WaitForSeconds(fadeInDuration + delayBetweenPhrases);
        yield return StartCoroutine(FadeOut(fadeOutDuration));
        
        yield return StartCoroutine(FadeIn("Te estaré esperando en las profundidades del inframundo.", fadeInDuration));
        yield return new WaitForSeconds(fadeInDuration + delayBetweenPhrases);
        yield return StartCoroutine(FadeOut(fadeOutDuration));

        StartCoroutine(ChangeSceneAfterDialogue());
    }

    private IEnumerator FadeIn(string phrase, float duration)
    {
        textElement.alpha = 0f;
        textElement.SetText(phrase);

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / duration);
            textElement.alpha = alpha;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textElement.alpha = 1f;
    }

    private IEnumerator FadeOut(float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            textElement.alpha = alpha;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textElement.alpha = 0f;
    }

    private IEnumerator ChangeSceneAfterDialogue()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Menu");
    }
}