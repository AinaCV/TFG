using Ink.Runtime;
using SaveLoadSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public GameObject menuReference;
    public static GameManager instance; //static--> para los demás componentes puedan acceder al GM
    //public bool hasItemsInInventory;
    public int numberOfItems;
    Story story;
    void Awake()
    {
        if (instance == null)
        {
            instance = this; //this = new GM que unity ha hecho por nosotros
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); //si hay más de un GM se queda el primero que se instancia, el otro se elimina y se queda entre escenas
        }
    }

    private void Update()
    {

        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if (!menuReference.activeInHierarchy)
        //    {
        //        menuReference.SetActive(true);
        //    }
        //    else if (menuReference.activeInHierarchy)
        //    {
        //        menuReference.SetActive(false);
        //    }
        //}
    }
}