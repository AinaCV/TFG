using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCanvas : MonoBehaviour
{
    public GameObject visualCue;

    private void Start()
    {
        visualCue.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            visualCue.SetActive(true); //activamos el icono si está dentro del trigger
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            visualCue.SetActive(false);
        }
    }
}
