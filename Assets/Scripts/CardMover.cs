using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMover : MonoBehaviour
{
    [SerializeField] private int speed;
    private int currentSpeed;
    private bool isMoving;
    private Vector3 startPosition;

    private Plane dragPlane;
    private Vector3 offset;
    private Camera mainCamera;

    private bool inTrigger;

    private void Awake()
    {
        mainCamera = Camera.main;
        isMoving = false;
        inTrigger = false;
        currentSpeed = speed;
    }

    private void Start()
    {
        startPosition = transform.position;
    }

    private void OnMouseDown()
    {
        isMoving = true;

        dragPlane = new Plane(mainCamera.transform.forward, transform.position);
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);

        float planeDistance;
        dragPlane.Raycast(cameraRay, out planeDistance);
        offset = transform.position - cameraRay.GetPoint(planeDistance);
    }

    private void OnMouseDrag()
    {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);

        float planeDistance;
        dragPlane.Raycast(cameraRay, out planeDistance);
        transform.position = cameraRay.GetPoint(planeDistance) + offset;
    }

    private void OnMouseUp()
    {
        isMoving = false;
        if (inTrigger)
        {
            gameObject.SetActive(false);
            //transform.GetComponent<>
        }
    }

    private void Update()
    {
        if (!isMoving && !(startPosition - transform.position == new Vector3(0, 0, 0)))
        {
            Vector3 moveVector = startPosition - transform.position;
            transform.Translate(Vector3.Normalize(moveVector) * currentSpeed * Time.deltaTime);
            if(Vector3.Distance(startPosition, transform.position) < 0.25)
            {
                currentSpeed = speed / 2;
            }
            if(Mathf.Abs(startPosition.x - transform.position.x) + Mathf.Abs(startPosition.y - transform.position.y) < 0.002)
            {
                transform.position = startPosition;
                currentSpeed = speed;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        inTrigger = true;
        Debug.Log("Enter trigger");
    }

    private void OnTriggerExit(Collider other)
    {
        inTrigger = false;
        Debug.Log("Exit trigger");
    }
}
