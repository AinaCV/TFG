using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    Animator anim;
    public GameObject gideon;

    private void Start()
    {
        anim = gideon.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            anim.Play("Lift");
        }
    }
}
