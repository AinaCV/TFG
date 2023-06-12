using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class FadeInText : MonoBehaviour
{
    public TMP_Text textElement;
    public float fadeInDuration = 8f;
    public float fadeOutDuration = 8f;
    public float delayBetweenPhrases = 5f;
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

        yield return StartCoroutine(FadeIn(""));
        yield return new WaitForSeconds(delayBetweenPhrases);
        yield return StartCoroutine(FadeOut());

        yield return StartCoroutine(FadeIn("Lilith"));
        yield return new WaitForSeconds(delayBetweenPhrases);
        yield return StartCoroutine(FadeOut());

        yield return StartCoroutine(FadeIn("Si quieres salvar a tu hermano, debes buscarme en el abismo."));
        yield return new WaitForSeconds(delayBetweenPhrases);
        yield return StartCoroutine(FadeOut());

        yield return StartCoroutine(FadeIn("Te estaré esperando"));
        yield return new WaitForSeconds(delayBetweenPhrases);
        yield return StartCoroutine(FadeOut());

        StartCoroutine(ChangeSceneAfterDialogue());
    }

    private IEnumerator FadeIn(string phrase)
    {
        textElement.alpha = 0f;
        textElement.SetText(phrase);

        float elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            textElement.alpha = alpha;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textElement.alpha = 1f;
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutDuration);
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