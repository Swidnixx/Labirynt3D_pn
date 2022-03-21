using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform receiver;

    bool playerOverlapping;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            print(receiver.position);
            playerOverlapping = true;
        }
    }

    private void FixedUpdate()
    {
        if (playerOverlapping)
        {
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);
            if(dotProduct < 0)
            {
                // pod warunkiem, ¿e teren jest p³aski
                player.position = new Vector3(receiver.position.x, player.position.y, receiver.position.z);
                // nie dzia³a przechodzenie ty³em
                player.rotation = Quaternion.LookRotation(receiver.up);
                playerOverlapping = false;
            }
        }
    }
    private void Update()
    {
        Debug.DrawLine(transform.position, player.position, Color.red);
    }

    void OnTriggerExit(Collider other)
    {
        playerOverlapping = false;
    }
}
