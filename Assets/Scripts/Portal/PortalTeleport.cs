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
            player.position = receiver.position;
            playerOverlapping = false; 
        }
    }
}
