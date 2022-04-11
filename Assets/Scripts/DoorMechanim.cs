using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMechanim : MonoBehaviour
{
    public Transform door;
    public Transform openPos;
    public Transform closedPos;
    public float speed = 1;
    public bool open = false;

    private void Update()
    {
        if (open && Vector3.Distance(door.position, openPos.position) > Mathf.Epsilon)
        {
            door.position =
        Vector3.MoveTowards(door.position, openPos.position, Time.deltaTime * speed); 
        }

        if (!open && Vector3.Distance(door.position, closedPos.position) > Mathf.Epsilon)
        {
            door.position =
        Vector3.MoveTowards(door.position, closedPos.position, Time.deltaTime * speed);
        }
    }
}
