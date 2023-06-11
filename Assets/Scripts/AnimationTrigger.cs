using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    public Animator anim;
    public GameObject gideon;

    private void Start()
    {
        anim = gideon.GetComponent<Animator>();
    }

    public void OnTriggerStay(Collider other)
    {
            anim.GetBool("playerInTrigger");
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            anim.SetBool("playerInTrigger", true);
            Debug.Log("Mensaje de depuración");
            //anim.Play("Lift");
        }
    }
}
