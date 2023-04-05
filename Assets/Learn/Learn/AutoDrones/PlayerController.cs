using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    public float forwardSpeed = 10;
    public float backSpeed = 4;
    public float rotationSpeed = 1;

    private Vector3 playerForward;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerForward = Vector3.forward;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            characterController.Move(playerForward * forwardSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            characterController.Move(-1 * playerForward * backSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            characterController.transform.Rotate(Vector3.down * rotationSpeed * Time.deltaTime);
            PlayerForwardUpdate();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            characterController.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            PlayerForwardUpdate();
        }
    }

    private void PlayerForwardUpdate()
    {
        float playerForwardX = Mathf.Sin(Mathf.Deg2Rad * characterController.transform.rotation.eulerAngles.y);
        float playerForwardZ = Mathf.Cos(Mathf.Deg2Rad * characterController.transform.rotation.eulerAngles.y);
        playerForward = new Vector3(playerForwardX, 0, playerForwardZ);
    }
}
