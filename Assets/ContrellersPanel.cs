using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContrellersPanel : MonoBehaviour
{
    public GameObject panel;
    public bool controllerPanelActive = true;
    public static ContrellersPanel Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void OkButton()
    {
        controllerPanelActive = false;
        panel.SetActive(false);
    }
}
