using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveFigure : MonoBehaviour
{
    [SerializeField] private GameObject figure;
    [SerializeField] private GameObject rowInDB;

    public void DeactivateFigure()
    {
        figure.SetActive(false);
        rowInDB.SetActive(false);
    }
}
