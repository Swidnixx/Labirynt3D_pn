using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public PortalCamera linkedPortalCamera;
    public MeshRenderer renderPlane;
    Camera playerCamera;
    Camera portalCamera;

    RenderTexture renderTexture;
    public Transform portalParent;

    private void Awake()
    {
        portalParent = transform.parent;
        playerCamera = Camera.main;
        portalCamera = GetComponent<Camera>();
    }

    void Start()
    {
        CreateRenderTexture();
    }

    void Update()
    {
        Matrix4x4 m = portalParent.localToWorldMatrix *
            linkedPortalCamera.portalParent.worldToLocalMatrix *
            playerCamera.transform.localToWorldMatrix;
        transform.SetPositionAndRotation(m.GetColumn(3), m.rotation);
    }

    void CreateRenderTexture()
    {
        if(renderTexture == null)
        {
            renderTexture = new RenderTexture(Screen.width, Screen.height, 0);
            portalCamera.targetTexture = renderTexture;
            linkedPortalCamera.renderPlane.material.SetTexture("_MainTex", renderTexture);
        }
    }
}
