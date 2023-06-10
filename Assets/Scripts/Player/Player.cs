using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    private CharacterController controller;
    public float speed = 5f;
    public float runSpeed = 10f;
    public float gravityforce = 50f;

    [Header("Health")]
    public int currentHealth;
    public int maxHealth = 10;
    public HealthBar healthBar;

    [Header("Stamina")]
    public float currentStamina;
    public float maxStamina = 10;
    public float recoverStaminaMaxTime = 5;
    public float recoverStamina;
    public StaminaBar staminaBar;

    [Header("Magic")]
    public float currentMagic;
    public float maxMagic = 10;
    public float recoverMagicMaxTime = 5;
    public float recoverMagic;
    public MagicBar magicBar;

    [Header("Bools")]
    public bool isRegenerating = false;
    public bool isRunning = false;
    public bool isWalking = false;

    public static Player Instance; //tiene que ser static para  health, stamina y magic bar

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        currentHealth = maxHealth;
        healthBar.SetMaxHaelth(maxHealth);
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
        currentMagic = maxMagic;
        magicBar.SetMaxMagic(maxMagic);
    }

    void Update()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying || Inventory.Instance.inventoryOnScreen)
        {
            return;
        }

        if (currentStamina < maxStamina && isRegenerating)
        {
            currentStamina += Time.deltaTime;
        }

        if (currentStamina > maxStamina)
        {
            currentStamina = maxStamina;
            isRegenerating = false;
        }

        if (currentStamina < 0)
        {
            currentStamina = 0;
        }

        if (Input.GetKey("left shift"))
        {
            isRunning = true;
            isRegenerating = false;
        }
        else
        {
            isRegenerating = true;
            isRunning = false;

        }

        Move();
        Stamina();
        //Death();
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void Move()
    {
        bool forwardPressed = Input.GetKey("w");
        bool leftPressed = Input.GetKey("a");
        bool backwardPressed = Input.GetKey("s");
        bool rightPressed = Input.GetKey("d");
        bool runPressed = Input.GetKey("left shift");

        if (runPressed && (forwardPressed || backwardPressed || rightPressed || leftPressed))
        {
            isRunning = true;
            isWalking = false;
        }
        else if (forwardPressed || backwardPressed || rightPressed || leftPressed)
        {
            isWalking = true;
            isRunning = false;
        }
        else
        {
            isWalking = false;
            isRunning = false;
        }

        if (isWalking)
        {

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 movement = transform.right * x + transform.forward * z + transform.up * -gravityforce;
            movement *= Time.deltaTime * speed;
            movement.y /= speed;
            controller.Move(movement);
        }
        else if (isRunning)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 movement = transform.right * x + transform.forward * z + transform.up * -gravityforce;
            movement *= Time.deltaTime * runSpeed;
            movement.y /= runSpeed;
            controller.Move(movement);
        }
    }

    void Stamina()
    {
        if (isRunning)
        {
            currentStamina -= Time.deltaTime;
            staminaBar.SetMaxStamina(currentStamina);

        }

        if (currentStamina <= 0)
        {
            isRunning = false;
        }

        if (isRunning == false)
        {
            recoverStamina += Time.deltaTime;
            if (recoverStamina >= recoverStaminaMaxTime)
            {
                isRegenerating = true;
                recoverStamina = 0;
            }
        }
    }


    //private void Death()
    //{
    //    if (health <= 0)
    //    {
    //        SceneManager.LoadScene("Menu");
    //        StartCoroutine(NewGameCoroutine());
    //    }
    //}
}
