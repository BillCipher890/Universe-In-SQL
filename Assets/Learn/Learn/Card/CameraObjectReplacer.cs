using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObjectReplacer : MonoBehaviour
{
    private Camera camera;
    [SerializeField] private GameObject cardPrefub;

    private void Start()
    {
        camera = GetComponent<Camera>();
    }

    void fixedUpdate()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("hit something");
            Transform objectHit = hit.transform;
            if (objectHit.gameObject == cardPrefub)
            {
                Debug.Log("hit card");
            }
        }
    }

    private void OnMouseDrag()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("hit something");
            Transform objectHit = hit.transform;
            objectHit.Translate(0.1f, 0, 0);
            if(objectHit.gameObject == cardPrefub)
            {
                Debug.Log("hit card");
            }
        }

        //gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    private void OnMouseUp()
    {
        //gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}
