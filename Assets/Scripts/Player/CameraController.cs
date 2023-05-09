using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Player player;
    public float mouseSens = 400f;
    private float yRotation = 0f;
    public float maxY;
    public float minY;
    //public GameObject followTransform;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // para que desaparezca el cursor
    }

    void Update()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            Cursor.lockState = CursorLockMode.None; // para que aparezca el cursor en los dialogos
        }
        else
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

            yRotation -= mouseY; // el menos es importante
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(yRotation, 0, 0), 1f); //Lerp = interpolacion, euler es un vector normal
                                                                                                                       //var angles = followTransform.transform.localEulerAngles;
                                                                                                                       //transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
                                                                                                                       //followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);

            player.transform.Rotate(Vector3.up * mouseX); //movimiento horizontal 

            if (yRotation >= maxY)
            {
                yRotation = maxY;
            }
            if (yRotation <= minY)
            {
                yRotation = minY;
            }
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}