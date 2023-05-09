using SaveLoadSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int level = 0;
    int auxLevel = 0;
    public int levelUpPoints = 0;
    public float xp = 0;
    public GameObject menuReference;
    public GameObject levelUpInterface;
    public static GameManager instance; //static--> para los demás componentes puedan acceder al GM

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
        if (xp >= 1)
        {
            level += 1;
            xp = 0;
        }
        if (auxLevel != level)
        {
            levelUpPoints += 1;
            auxLevel = level;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!levelUpInterface.activeInHierarchy)
            {
                levelUpInterface.SetActive(true);
            }
            else if (levelUpInterface.activeInHierarchy)
            {
                //levelUpInterface.GetComponent<LevelUpInterface>().healthDown.enabled = false;
                //levelUpInterface.GetComponent<LevelUpInterface>().staminaDown.enabled = false;
                //levelUpInterface.GetComponent<LevelUpInterface>().damageDown.enabled = false;
                levelUpInterface.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menuReference.activeInHierarchy)
            {
                menuReference.SetActive(true);
            }
            else if (menuReference.activeInHierarchy)
            {
                menuReference.SetActive(false);
            }
        }
    }
}
