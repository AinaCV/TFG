using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Player player;
    public Transform cameraPivot;

    public float mouseSens = 400f;
    public float smoothTime = 0.2f;
    public float maxY;
    public float minY;
    public float rotationSpeed = 5f;
    public bool isMoving = false;
    public Transform target;
    private float yRotation = 0f;
    private float xRotation = 0f;
    private Vector3 lastCameraPosition;
    private Vector3 currentVelocity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // para que desaparezca el cursor
        lastCameraPosition = cameraPivot.position;
    }

    void Update()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying || Inventory.Instance.inventoryOnScreen)
        {
            Cursor.lockState = CursorLockMode.None; // para que aparezca el cursor en los dialogos
            return;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (player.isWalking || player.isRunning)

        {

            float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

            yRotation -= mouseY; // el menos es importante
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(yRotation, 0, 0), 1f);

            player.transform.Rotate(Vector3.up * mouseX); //movimiento horizontal 
            isMoving = true;
        }
        else
        {
            isMoving = false;
            float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

            yRotation -= mouseY; // el menos es importante
            xRotation += mouseX; // el menos es importante

            yRotation = Mathf.Clamp(yRotation, minY, maxY); // límites para yRotation
            //Quaternion camTargetRotation = Quaternion.Euler(yRotation, xRotation, 0); // Usar ambas rotaciones
            //transform.localRotation = Quaternion.Lerp(transform.localRotation, camTargetRotation, 1f);
            cameraPivot.RotateAround(player.transform.position, Vector3.up, mouseX * rotationSpeed);
            cameraPivot.rotation = Quaternion.Euler(yRotation, cameraPivot.rotation.eulerAngles.y, 0);
        }

        if (player.isWalking || player.isRunning)
        {
            transform.LookAt(player.transform);
            isMoving = true;
        }

        if (!player.isWalking || !player.isRunning)
        {
            transform.LookAt(target.transform);
            isMoving = false;
            //AudioManager.Instance.PlayAudio(clip.name);
        }

        ChangeBoolState();

        if (yRotation >= maxY)
        {
            yRotation = maxY;
        }
        if (yRotation <= minY)
        {
            yRotation = minY;
        }
    }
    void ChangeBoolState()
    {
        isMoving = !isMoving; //comprueba en que estado se encuentra el bool y lo cambia al pulsar la tecla

        // Iniciar el movimiento suave de la cámara hacia la última posición cuando el jugador deja de moverse
        cameraPivot.position = Vector3.SmoothDamp(cameraPivot.position, lastCameraPosition, ref currentVelocity, smoothTime);
        // Actualizar la última posición de la cámara cuando el jugador comienza a moverse
        lastCameraPosition = cameraPivot.position;
    }

    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}