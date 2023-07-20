using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacultyController : MonoBehaviour
{
    public enum Faculty
    {
        Gryffindor,
        Slytherin,
        Ravenclaw,
        Hufflepuff
    }

    [SerializeField] public Faculty currentFaculty;

    [SerializeField] private Material Gryffindor;
    [SerializeField] private Material Slytherin;
    [SerializeField] private Material Ravenclaw;
    [SerializeField] private Material Hufflepuff;

    void Start()
    {
        switch (currentFaculty)
        {
            case Faculty.Gryffindor:
                transform.Find("Body").Find("hood").GetComponent<MeshRenderer>().material = Gryffindor;
                break;

            case Faculty.Slytherin:
                transform.Find("Body").Find("hood").GetComponent<MeshRenderer>().material = Slytherin;
                break;

            case Faculty.Ravenclaw:
                transform.Find("Body").Find("hood").GetComponent<MeshRenderer>().material = Ravenclaw;
                break;

            case Faculty.Hufflepuff:
                transform.Find("Body").Find("hood").GetComponent<MeshRenderer>().material = Hufflepuff;
                break;
        }
    }
}
