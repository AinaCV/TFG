using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private bool animationFinished = false;
    public GameObject NPC_01;
    public Player player;
    [SerializeField] private TextAsset inkJSON;
    private float rotationSpeed = 1f;
    public bool specialEndCalled = false;
    public Transform npcTarget;
    bool dialogueExecuted = false;
    public GameObject enemyDialogue;

    private void Start()
    {
        enemyDialogue.SetActive(false);
    }

    public void AnimationFinished()
    {
        animationFinished = true;
    }

    public void Update()
    {
        if (animationFinished && DialogueManager.GetInstance().hasGivenItem)
        {
            specialEndCalled = true;
        }

        if (animationFinished && !DialogueManager.GetInstance().hasGivenItem)
        {
            enemyDialogue.SetActive(true);
            //ChangeSceneAfterAnim();
        }
        else if (specialEndCalled)
        {
            if (!dialogueExecuted)
            {
                SpecialEnd();
                dialogueExecuted = true;
            }
        }
    }

    //private void ChangeSceneAfterAnim()
    //{
    //    SceneManager.LoadScene("End");
    //}

    public void SpecialEnd()
    {
        NPC_01.transform.position = new Vector3(-89.45f, 1.64f, 73.73f);
        NPC_01.transform.rotation = new Quaternion(-6.16e-05f, -0.97f, -0.00f, 0.23f);
        DialogueTrigger trigger = NPC_01.GetComponentInChildren<DialogueTrigger>();
        if (trigger)
        {
            trigger.enabled = false;
        }

        Vector3 direction = npcTarget.position - player.transform.position;
        direction.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        player.transform.rotation = Quaternion.Lerp(player.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        StartCoroutine(ChangeSceneCoroutine());
    }

    private IEnumerator ChangeSceneCoroutine()
    {
        yield return new WaitForSeconds(5);
        enemyDialogue.SetActive(true);
    }
}