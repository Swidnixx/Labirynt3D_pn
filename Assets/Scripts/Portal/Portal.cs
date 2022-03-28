using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Portal linkedPortal;

    public PortalCamera portalCamera;
    [HideInInspector]
    public PortalTeleport portalTeleport;
    Transform player;

    private void Awake()
    {
        portalCamera = GetComponentInChildren<PortalCamera>();
        portalTeleport = GetComponentInChildren<PortalTeleport>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        portalCamera.linkedPortalCamera = linkedPortal.portalCamera;
    }

    private void Start()
    {

        portalTeleport.receiver = linkedPortal.portalTeleport.transform;
        portalTeleport.player = player;
    }
}
