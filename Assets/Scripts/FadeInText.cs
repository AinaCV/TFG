using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class FadeInText : MonoBehaviour
{
    public TMP_Text textElement;
    public float fadeInDuration = 0.5f;
    public float fadeOutDuration = 0.5f;
    public float delayBetweenPhrases = 1f;
    private bool showDialogue = false;
    private bool isShowingPhrase = false;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!showDialogue && !isShowingPhrase)
        {
            gameObject.SetActive(true);
            StartCoroutine(ShowPhrases());
        }
    }

    public IEnumerator ShowPhrases()
    {
        showDialogue = true;
        textElement.gameObject.SetActive(true);

        isShowingPhrase = true;
        yield return StartCoroutine(FadeIn(""));
        yield return new WaitForSeconds(delayBetweenPhrases);
        yield return StartCoroutine(FadeOut());
        isShowingPhrase = false;

        isShowingPhrase = true;
        yield return StartCoroutine(FadeIn("Lilith"));
        yield return new WaitForSeconds(delayBetweenPhrases);
        yield return StartCoroutine(FadeOut());
        isShowingPhrase = false;

        isShowingPhrase = true;
        yield return StartCoroutine(FadeIn("Si quieres salvar a tu hermano, debes buscarme en el abismo."));
        yield return new WaitForSeconds(delayBetweenPhrases);
        yield return StartCoroutine(FadeOut());
        isShowingPhrase = false;

        isShowingPhrase = true;
        yield return StartCoroutine(FadeIn("Te estaré esperando"));
        yield return new WaitForSeconds(delayBetweenPhrases);
        yield return StartCoroutine(FadeOut());
        isShowingPhrase = false;
        

        showDialogue = false;

        Debug.Log(showDialogue);
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

        // Desactivar el panel de diálogo después de mostrar la última frase
        if (!isShowingPhrase)
        {
            gameObject.SetActive(false);
        }
    }

    private IEnumerator ChangeSceneAfterDialogue()
    {
        yield return new WaitForSeconds(2f); // Agrega un tiempo de espera antes de cambiar de escena si es necesario
        SceneManager.LoadScene("Menu");
    }
}

//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//using TMPro;

//public class FadeInText : MonoBehaviour
//{
//    public TMP_Text textElement;
//    public float fadeInDuration = 0.5f;
//    public float fadeOutDuration = 0.5f;
//    public float delayBetweenPhrases = 1f;
//    private bool showDialogue = false;
//    private bool hasBeenPlayed = false;

//    private void Start()
//    {
//        gameObject.SetActive(false);
//    }

//    private void Update()
//    {
//        if (hasBeenPlayed && showDialogue)
//        {
//            StopAllCoroutines();
//            showDialogue = false;
//        }
//        else if (!showDialogue)
//        {
//            StartCoroutine(ShowPhrases());
//        }
//    }

//    public IEnumerator ShowPhrases()
//    {
//        textElement.gameObject.SetActive(true);
//        hasBeenPlayed = true;

//        while (!showDialogue)
//        {
//            yield return StartCoroutine(FadeIn("Lilith"));
//            yield return new WaitForSeconds(delayBetweenPhrases);
//            yield return StartCoroutine(FadeOut());

//            yield return StartCoroutine(FadeIn("Si quieres salvar a tu hermano, debes buscarme en el abismo."));
//            yield return new WaitForSeconds(delayBetweenPhrases);
//            yield return StartCoroutine(FadeOut());

//            yield return new WaitForSeconds(delayBetweenPhrases);
//            yield return StartCoroutine(FadeOut());
//            yield return new WaitForSeconds(delayBetweenPhrases);
//            hasBeenPlayed = true;
//        }

//        textElement.gameObject.SetActive(false);
//        Debug.Log(showDialogue);
//    }

//    private IEnumerator FadeIn(string phrase)
//    {
//        textElement.alpha = 0f;
//        textElement.SetText(phrase);

//        float elapsedTime = 0f;
//        while (elapsedTime < fadeInDuration)
//        {
//            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
//            textElement.alpha = alpha;
//            elapsedTime += Time.deltaTime;
//            yield return null;
//        }

//        textElement.alpha = 1f;
//    }

//    private IEnumerator FadeOut()
//    {
//        float elapsedTime = 0f;
//        while (elapsedTime < fadeOutDuration)
//        {
//            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutDuration);
//            textElement.alpha = alpha;
//            elapsedTime += Time.deltaTime;
//            yield return null;
//        }

//        textElement.alpha = 0f;
//    }
//}