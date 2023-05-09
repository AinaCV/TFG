using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 5f;
    public float runSpeed = 10f;
    public float gravityforce = 50f;
    public int health;
    public int maxHealth = 10;
    public float stamina;
    public float maxStamina = 10;
    public float recoverStaminaMaxTime = 5;
    public float recoverStamina;
    private Vector3 targetDirection;
    //Animator anim;
    [Header("Bools")]
    public bool isRegenerating = false;
    public bool isRunning = false;
    public bool isWalking = false;

    [Header("Axis")]
    private Vector2 input;
    private Quaternion freeRotation;
    private Vector3 targetDir;

    public static Player Instance; //tiene que ser static para la health y stamina bar

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        health = maxHealth;
        stamina = maxStamina;
    }

    void Update()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            //anim.GetComponent<AnimationController>();
            //anim.Play("Idle");
            return;
        }

        if (stamina < maxStamina && isRegenerating)
        {
            stamina += Time.deltaTime;
        }

        if (stamina > maxStamina)
        {
            stamina = maxStamina;
            isRegenerating = false;
        }

        if (stamina < 0)
        {
            stamina = 0;
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
        //Stamina();
        //Death();
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

        UpdateTargetDirection();

        if (input != Vector2.zero && targetDir.magnitude > 0.1f)
        {
            Vector3 lookDir = targetDirection.normalized;
            freeRotation = Quaternion.LookRotation(lookDir, transform.up);
            var diferenceRotation = freeRotation.eulerAngles.y - transform.eulerAngles.y;
            var eulerY = transform.eulerAngles.y;
            if (diferenceRotation < 0 || diferenceRotation > 0)
            {
                eulerY = freeRotation.eulerAngles.y;
            }
            var eurler = new Vector3(0, eulerY, 0);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(eurler), runSpeed * Time.deltaTime);
        }

    }

    public void UpdateTargetDirection()
    {
        var forward = Camera.main.transform.TransformDirection(Vector3.forward);
        forward.y = 0;

        var right = Camera.main.transform.TransformDirection(Vector3.right);

        targetDirection = input.x * right + input.y * forward;
    }

    //void Stamina()
    //{
    //    if (isRunning)
    //    {
    //        stamina -= Time.deltaTime;
    //    }

    //    if (stamina <= 0)
    //    {
    //        isRunning = false;
    //    }

    //    if (isRunning == false)
    //    {
    //        recoverStamina += Time.deltaTime;
    //        if (recoverStamina >= recoverStaminaMaxTime)
    //        {
    //            isRegenerating = true;
    //            recoverStamina = 0;
    //        }
    //    }
    //}


    //private void Death()
    //{
    //    if (health <= 0)
    //    {
    //        SceneManager.LoadScene("Menu");
    //        StartCoroutine(NewGameCoroutine());
    //    }
    //}

    //IEnumerator NewGameCoroutine()
    //{
    //    yield return new WaitForSeconds(2.0f);
    //}
}
