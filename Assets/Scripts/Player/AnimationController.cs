using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator anim;
    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public float maxWalkVelocity = 0.5f;
    public float maxRunVelocity = 2.0f;
    float coolDown = 1f;
    //public float walkTimer;
    public float runTimer;
    //const float coolDownWalk = 1f;
    //const float coolDownRun = 1F;

    //public AudioClip walk;
    //public AudioClip run;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying && velocityX != 0.0f && velocityZ != 0.0f || Inventory.Instance.inventoryOnScreen)
        {
            velocityX = 0.0f;
            velocityZ = 0.0f;
            //anim.Play("Idle");
            return;
        }
        else
        {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool righhtPressed = Input.GetKey(KeyCode.D);
        bool backwardsPressed = Input.GetKey(KeyCode.S);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        //Ternary operator
        float currentMaxVelocity = runPressed ? maxRunVelocity : maxWalkVelocity;// currentMaxVelocity es la maxRun o la maxWalk dependiendo se si pulsas shift 
                                                                                 //la primera opcion se cumple si pulso shift, si no, la segunda
            Animation(forwardPressed, leftPressed, righhtPressed, backwardsPressed, runPressed, currentMaxVelocity);
            //walkTimer -= Time.deltaTime;
        }
    }

    void Animation(bool forwardPressed, bool leftPressed, bool righhtPressed, bool backwardsPressed, bool runPressed, float currentMaxVelocity)
    {

        //if (Player.Instance.stamina <= 0)
        //{
        currentMaxVelocity = maxWalkVelocity;
        //}

        //if (forwardPressed || leftPressed || righhtPressed || backwardsPressed)
        //{
        //    //if (walkTimer <= 0)
        //    //{
        //    //    AudioManager.Instance.PlayAudio(walk, 0.5f);
        //    //    walkTimer = coolDownWalk;
        //}
        //else if (runPressed)
        //{
        //    if (runTimer <= 0)
        //    {
        //        AudioManager.Instance.PlayAudio(run, 1f);
        //        runTimer = coolDownRun;
        //    }
        //}


        if (forwardPressed && velocityZ < currentMaxVelocity) //walk
        {
            velocityZ += Time.deltaTime * acceleration;
        }

        if (backwardsPressed && velocityZ > -currentMaxVelocity) //BACKWARDS
        {
            velocityZ -= Time.deltaTime * acceleration;
        }

        if (leftPressed && velocityX > -currentMaxVelocity) //left strafe Walk
        {
            velocityX -= Time.deltaTime * acceleration;
        }

        if (righhtPressed && velocityX < currentMaxVelocity) //right strafe Walk
        {
            velocityX += Time.deltaTime * acceleration;
        }

        if (!forwardPressed && velocityZ > 0.0f) //si no avanza decelera
        {
            velocityZ -= Time.deltaTime * deceleration;
        }

        if (!backwardsPressed && velocityZ < 0.0f) //si no avanza hacia atrás restablece el valor a 0
        {
            velocityZ += Time.deltaTime * acceleration;
        }

        if (!leftPressed && velocityX < 0.0f) //sube velocidad si no estoy pulsando left y la velocidad en x < 0
        {
            velocityX += Time.deltaTime * deceleration;
        }

        if (!righhtPressed && velocityX > 0.0f) //baja velocidad si no estoy pulsando right y la velocidad en x > 0
        {
            velocityX -= Time.deltaTime * deceleration;
        }

        if (!leftPressed && !righhtPressed && velocityX != 0.0f && (velocityX > -0.05f && velocityX < 0.05f))//Resetea velocidad
        {
            velocityX = 0.0f;
        }

        if (forwardPressed && runPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ = currentMaxVelocity;
        }
        else if (forwardPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * deceleration;
            if (velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05f))//si nos acercamos a currentMaxVelocity pones el valor a currentMaxVelocity
            {
                velocityZ = currentMaxVelocity;
            }
        }
        else if (forwardPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05f)) //si nos acercamos a currentMaxVelocity pones el valor a currentMaxVelocity
        {
            velocityZ = currentMaxVelocity;
        }

        if (backwardsPressed && runPressed && velocityZ < -currentMaxVelocity)
        {
            velocityZ = -currentMaxVelocity;
        }
        else if (backwardsPressed && velocityZ < -currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * deceleration;
            if (velocityZ < -currentMaxVelocity && velocityZ > (-currentMaxVelocity - 0.05f))
            {
                velocityZ = currentMaxVelocity;
            }
        }
        else if (backwardsPressed && velocityZ > -currentMaxVelocity && velocityZ < (-currentMaxVelocity + 0.05f))
        {
            velocityZ = -currentMaxVelocity;
        }

        if (leftPressed && runPressed && velocityX < -currentMaxVelocity)
        {
            velocityX = -currentMaxVelocity;
        }
        else if (leftPressed && velocityX < -currentMaxVelocity)
        {
            velocityX += Time.deltaTime * deceleration;
            if (velocityX < -currentMaxVelocity && velocityX > (-currentMaxVelocity - 0.05f))
            {
                velocityX = currentMaxVelocity;
            }
        }
        else if (leftPressed && velocityX > -currentMaxVelocity && velocityX < (-currentMaxVelocity + 0.05f))
        {
            velocityX = -currentMaxVelocity;
        }

        if (righhtPressed && runPressed && velocityX > currentMaxVelocity)
        {
            velocityX = currentMaxVelocity;
        }
        else if (righhtPressed && velocityX > currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * deceleration;
            if (velocityX > currentMaxVelocity && velocityX < (currentMaxVelocity + 0.05f))
            {
                velocityX = currentMaxVelocity;
            }
        }
        else if (righhtPressed && velocityX < currentMaxVelocity && velocityX > (currentMaxVelocity - 0.05f))
        {
            velocityX = currentMaxVelocity;
        }

        //if (jumpPressed)
        //{
        //    anim.SetBool("isJumping", true);
        //    if (jumpTimer <= 0)
        //    {
        //        //AudioManager.Instance.PlayAudio(jump, 1f);
        //        jumpTimer = coolDown;
        //    }
        //}
        //else
        //{
        //    anim.SetBool("isJumping", false);
        //}

        //if (Input.GetMouseButtonDown(0) && this.GetComponent<Player>().stamina > 0) //Attack
        //{
        //    anim.SetBool("Attack", true);
        //}
        //else
        //{
        //    anim.SetBool("Attack", false);
        //}

        //if (Input.GetMouseButton(1) && this.GetComponent<Player>().stamina > 0) //Block
        //{
        //    anim.SetBool("isBlocking", true);
        //}
        //else
        //{
        //    anim.SetBool("isBlocking", false);
        //}
        anim.SetFloat("Velocity Z", velocityZ);
        anim.SetFloat("Velocity X", velocityX);
    }

}