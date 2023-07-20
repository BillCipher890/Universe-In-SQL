using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    void Start()
    {
        Camera mainCamera = Camera.main;
        transform.rotation = mainCamera.transform.rotation;
    }
}
