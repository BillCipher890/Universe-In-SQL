using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    [SerializeField] private Material slytherinMaterial;
    [SerializeField] private Material gryffindorMaterial;

    private void Start()
    {
        EventManagerLevel1.onSlytherinWin += SetFlagSlytherin;
        EventManagerLevel1.onGryffindorWin += SetFlagGryffindor;
        gameObject.SetActive(false);
        transform.GetComponent<MeshRenderer>().material = slytherinMaterial;
    }

    private void OnDestroy()
    {
        EventManagerLevel1.onSlytherinWin -= SetFlagSlytherin;
        EventManagerLevel1.onGryffindorWin -= SetFlagGryffindor;
    }

    public void SetFlagSlytherin()
    {
        Debug.Log("catch slytherin win");
        gameObject.SetActive(true);
    }

    public void SetFlagGryffindor()
    {
        transform.GetComponent<MeshRenderer>().material = gryffindorMaterial;
    }
}
