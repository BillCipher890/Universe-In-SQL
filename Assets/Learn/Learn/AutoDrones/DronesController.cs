using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronesController : MonoBehaviour
{
    private CharacterController characterController;
    public float startSpeed = 2;
    public float yOffsetMin = 1.4f;
    public float yOffsetMax = 1.6f;

    [SerializeField] private Transform player;
    private Vector3 moveVector;
    private float yOffset;
    private bool chAtThisTime;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        MoveVectorUpdate();
        yOffset = Random.Range(yOffsetMin, yOffsetMax);
        chAtThisTime = true;
    }

    void Update()
    {
        MoveVectorUpdate();
        characterController.transform.Translate(moveVector * startSpeed * Time.deltaTime);
    }

    void MoveVectorUpdate()
    {
        if (Mathf.Floor(Time.timeSinceLevelLoad) % 5 == 0 && chAtThisTime)
        {
            yOffset = Random.Range(yOffsetMin, yOffsetMax);
            chAtThisTime = false;
        }
        if(Mathf.Floor(Time.timeSinceLevelLoad) % 5 == 1)
        {
            chAtThisTime = true;
        }
        float x = player.position.x - transform.position.x;
        float y = player.position.y - transform.position.y + yOffset;
        float z = player.position.z - transform.position.z;
        moveVector = new Vector3(x, y, z);
    }
}
