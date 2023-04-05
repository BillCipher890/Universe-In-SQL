using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardChooser : MonoBehaviour
{
    private Vector3 pointScreen;
    
    void OnMouseDown()
    {
        //gameObject.SetActive(false);
        
    }

    private void OnMouseDrag()
    {
        //Debug.Log("x:" + Input.GetAxis("Mouse X") + ", y:" + Input.GetAxis("Mouse Y"));
        //Vector3 vectorMouse = new Vector3(Input.GetAxis("Mouse X") * Time.deltaTime, Input.GetAxis("Mouse Y") * Time.deltaTime, 0);
        //transform.Translate(vectorMouse);
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    private void OnMouseUp()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;  
    }
}
